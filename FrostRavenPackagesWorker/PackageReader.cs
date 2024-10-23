using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace FrostRavenPackagesWorker
{
    public static class PackageReader
    {

        public static MemoryStream UnpackFile(string packagePath, string password, string relPath)
        {
            byte[] encryptedData = File.ReadAllBytes(packagePath);
            byte[] decryptedData = Decrypt(encryptedData, password);

            return GetFileStream(decryptedData, relPath);
        }

        public static MemoryStream UnpackFile(Package package, string relPath)
        {
            byte[] decryptedData;

            if (package.IsPreloaded() && package.IsDecrypted())
            {
                decryptedData = package.GetData();
            }
            else
            {
                decryptedData = package.GetDecryptedData();
            }

            return GetFileStream(decryptedData, relPath);
        }

        private static MemoryStream GetFileStream(byte[] decryptedData, string relPath)
        {
            using (MemoryStream ms = new MemoryStream(decryptedData))
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Read))
                {
                    ZipArchiveEntry entry = zip.GetEntry(relPath);
                    if (entry == null)
                    {
                        throw new FileNotFoundException($"File \"{relPath}\" doesn't exist in package!");
                    }
                    MemoryStream entryStream = new MemoryStream();
                    using (Stream stream = entry.Open())
                    {
                        stream.CopyTo(entryStream);
                    }
                    entryStream.Position = 0;
                    return entryStream;
                }
            }
        }

        internal static byte[] Decrypt(byte[] data, string password)
        {
            using(Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(PackagesConstants.SaltValue), PackagesConstants.Iterations);
                aes.Key = key.GetBytes(32);
                aes.IV = key.GetBytes(16);

                using(ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    using(MemoryStream ms = new MemoryStream(data))
                    {
                        using(CryptoStream cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using(MemoryStream result = new MemoryStream())
                            {
                                cryptoStream.CopyTo(result);
                                return result.ToArray();
                            }
                        }
                    }
                }
            }
        }

    }
}
