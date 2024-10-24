using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace FrostRavenPackagesWorker
{
    public class Package
    {
        public string FilePath;
        public string Password;

        private byte[] _data;
        private byte[] _editorData;
        private bool _isDecrypted = false;

        public Package(string filePath, string password)
        {
            FilePath = filePath;
            Password = password;
        }

        public void OpenForEditing() => _editorData = GetDecryptedData();
        public void CloseForEditing() => _editorData = null;

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

        public List<string> GetListOfFilesAndDirectories(string relPath)
        {
            List<string> items = new List<string>();
            using(MemoryStream ms = new MemoryStream(_editorData))
            {
                using(ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Read))
                {
                    foreach(ZipArchiveEntry entry in zip.Entries)
                    {
                        if (entry.FullName.StartsWith(relPath))
                        {
                            items.Add(entry.FullName);
                        }
                    }
                }
            }
            return items;
        }

        public void AddFile(string pathToFile, string relPath)
        {
            using(MemoryStream ms = new MemoryStream(_editorData))
            {
                using(ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Update))
                {
                    zip.CreateEntryFromFile(pathToFile, relPath);
                }
                _editorData = ms.ToArray();
            }
        }

        public void RemoveFile(string relPath)
        {
            using(MemoryStream ms = new MemoryStream(_editorData))
            {
                using(ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry entry = zip.GetEntry(relPath);
                    if (entry != null)
                    {
                        entry.Delete();
                    }
                }
                _editorData = ms.ToArray();
            }
        }

        public void CreateDirectory(string relPath)
        {
            using(MemoryStream ms = new MemoryStream(_editorData))
            {
                using(ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Update))
                {
                    zip.CreateEntry(relPath + "/");
                }
                _editorData = ms.ToArray();
            }
        }

        public void RemoveDirectory(string relPath)
        {
            using(MemoryStream ms = new MemoryStream(_editorData))
            {
                using(ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Update))
                {
                    foreach(ZipArchiveEntry entry in zip.Entries)
                    {
                        if(entry.FullName.StartsWith(relPath + "/"))
                        {
                            entry.Delete();
                        }
                    }
                }
                _editorData = ms.ToArray();
            }
        }

        public void SaveChanges(byte[] data, string savePath = null, string password = null)
        {
            if(savePath == null)
            {
                savePath = FilePath;
            }
            if(password == null)
            {
                password = Password;
            }
            byte[] encryptedData = PackageCreator.Encrypt(data, password);
            File.WriteAllBytes(savePath, encryptedData);
        }

        public void SaveChanges(string savePath = null, string password = null)
        {
            if (savePath == null)
            {
                savePath = FilePath;
            }
            if (password == null)
            {
                password = Password;
            }
            File.WriteAllBytes(savePath, PackageCreator.Encrypt(_editorData, password));
        }

    }
}
