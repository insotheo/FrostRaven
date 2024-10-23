using System.IO;

namespace FrostRavenPackagesWorker
{
    public class Package
    {
        public string FilePath;
        public string Password;

        private byte[] _data;
        private bool _isDecrypted = false;

        public Package(string filePath, string password)
        {
            FilePath = filePath;
            Password = password;
        }

        public void Preload() => _data = GetData();
        public void PreloadAndDecrypt() 
        { 
            _data = GetDecryptedData();
            _isDecrypted = true;
        }
        public void Unload() => _data = null;
        public bool IsPreloaded() => _data != null;
        public bool IsDecrypted() => _isDecrypted;

        public byte[] GetData()
        {
            if (_data == null)
            {
                return File.ReadAllBytes(FilePath);
            }
            return _data;
        }

        internal byte[] GetDecryptedData() => PackageReader.Decrypt(GetData(), Password);
    }
}
