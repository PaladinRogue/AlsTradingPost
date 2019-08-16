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
            string fullPath,
            KeyManagementServiceClient kms = null,
            IFileProvider innerProvider = null)
        {
            _kms = kms ?? KeyManagementServiceClient.Create();
            _innerProvider = innerProvider ?? new PhysicalFileProvider(fullPath);
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