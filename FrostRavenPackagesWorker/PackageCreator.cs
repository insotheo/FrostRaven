using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace FrostRavenPackagesWorker
{
    public static class PackageCreator
    {
        public static void MakePackageWithFiles(string dirPath, string password, string outputName, string outputPath)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using(ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                {
                    AddFilesToZip(zip, dirPath, "");

                    ms.Position = 0;
                    byte[] encryptedData = Encrypt(ms.ToArray(), password);
                    File.WriteAllBytes(Path.Combine(outputPath, outputName + ".gamepac"), encryptedData);
                }
            }
        }

        private static void AddFilesToZip(ZipArchive zip, string sourceDir, string relPath)
        {
            foreach(string file in Directory.GetFiles(sourceDir))
            {
                zip.CreateEntryFromFile(file, Path.Combine(relPath, Path.GetFileName(file)));
            }
            foreach(string dir in Directory.GetDirectories(sourceDir))
            {
                AddFilesToZip(zip, dir, Path.Combine(relPath, Path.GetFileName(dir)));
            }
        }

        internal static byte[] Encrypt(byte[] data, string password)
        {
            using(Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(PackagesConstants.SaltValue), PackagesConstants.Iterations);
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                using(ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    using(MemoryStream ms = new MemoryStream())
                    {
                        using(CryptoStream cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(data, 0, data.Length);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

    }
}
