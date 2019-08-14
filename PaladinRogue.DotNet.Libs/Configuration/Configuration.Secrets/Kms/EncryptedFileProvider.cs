using System.IO;
using Google.Cloud.Kms.V1;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace PaladinRogue.Library.Configuration.Secrets.Kms
{
    public class EncryptedFileProvider : IFileProvider
    {
        private readonly KeyManagementServiceClient _kms;
        private readonly IFileProvider _innerProvider;

        public EncryptedFileProvider(
            KeyManagementServiceClient kms = null,
            IFileProvider innerProvider = null)
        {
            _kms = kms ?? KeyManagementServiceClient.Create();
            if (innerProvider == null)
            {
                string fullPath = System.Reflection.Assembly
                    .GetAssembly(typeof(EncryptedFileProvider)).Location;
                string directory = Path.GetDirectoryName(fullPath);
                _innerProvider = new PhysicalFileProvider(directory);
            }
            else
            {
                _innerProvider = innerProvider;
            }
        }

        public IDirectoryContents GetDirectoryContents(string subPath)
        {
            IDirectoryContents innerContents = _innerProvider.GetDirectoryContents(subPath);
            return innerContents == null ? null : new EncryptedDirectoryContents(_kms, innerContents);
        }

        public IFileInfo GetFileInfo(string subPath)
        {
            return EncryptedFileInfo.FromFileInfo(_kms,
                _innerProvider.GetFileInfo(subPath),
                _innerProvider.GetFileInfo(Path.ChangeExtension(subPath, ".keyName")));
        }

        public IChangeToken Watch(string filter)
        {
            return _innerProvider.Watch(filter);
        }
    }
}