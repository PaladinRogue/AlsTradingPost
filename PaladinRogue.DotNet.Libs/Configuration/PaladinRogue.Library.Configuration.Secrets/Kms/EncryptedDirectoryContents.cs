using System.Collections;
using System.Collections.Generic;
using System.IO;
using Google.Cloud.Kms.V1;
using Microsoft.Extensions.FileProviders;

namespace PaladinRogue.Library.Configuration.Secrets.Kms
{
    public class EncryptedDirectoryContents : IDirectoryContents
    {
        private readonly KeyManagementServiceClient _kms;
        private readonly IDirectoryContents _innerDirectoryContents;

        public EncryptedDirectoryContents(
            KeyManagementServiceClient kms,
            IDirectoryContents innerDirectoryContents)
        {
            _kms = kms;
            _innerDirectoryContents = innerDirectoryContents;
        }

        public bool Exists => _innerDirectoryContents.Exists;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            Dictionary<string, IFileInfo> keyNameFiles = new Dictionary<string, IFileInfo>();
            List<IFileInfo> encryptedFiles = new List<IFileInfo>();
            foreach (IFileInfo fileInfo in _innerDirectoryContents)
            {
                if (fileInfo.IsDirectory)
                {
                    yield return fileInfo;
                }
                else
                {
                    string extension = Path.GetExtension(fileInfo.Name);
                    switch (extension)
                    {
                        case ".encrypted":
                            encryptedFiles.Add(fileInfo);
                            break;
                        case ".keyName":
                            keyNameFiles[fileInfo.Name] = fileInfo;
                            break;
                    }
                }
            }

            foreach (IFileInfo fileInfo in encryptedFiles)
            {
                IFileInfo keyNameFile = keyNameFiles.GetValueOrDefault(
                    Path.ChangeExtension(fileInfo.Name, ".keyName"));
                if (keyNameFile != null)
                {
                    yield return EncryptedFileInfo.FromFileInfo(_kms, fileInfo,
                        keyNameFile);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerator<IFileInfo> x = GetEnumerator();
            return x;
        }
    }
}