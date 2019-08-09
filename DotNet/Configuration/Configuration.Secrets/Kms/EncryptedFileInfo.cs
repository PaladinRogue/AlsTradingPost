using System;
using System.IO;
using Google.Cloud.Kms.V1;
using Google.Protobuf;
using Microsoft.Extensions.FileProviders;

namespace Configuration.Secrets.Kms
{
    public class EncryptedFileInfo : IFileInfo
    {
        private readonly KeyManagementServiceClient _kms;
        private readonly Lazy<CryptoKeyName> _cryptoKeyName;
        private readonly IFileInfo _innerFileInfo;

        public static IFileInfo FromFileInfo(
            KeyManagementServiceClient kms,
            IFileInfo fileInfo,
            IFileInfo keyNameFileInfo)
        {
            if (fileInfo == null)
            {
                return null;
            }

            if (fileInfo.IsDirectory)
            {
                return fileInfo;
            }

            if (!fileInfo.Name.EndsWith(".encrypted"))
            {
                return null;
            }

            return new EncryptedFileInfo(kms, fileInfo, keyNameFileInfo);
        }

        private EncryptedFileInfo(
            KeyManagementServiceClient kms,
            IFileInfo innerFileInfo,
            IFileInfo keyNameFileInfo)
        {
            _kms = kms;
            _innerFileInfo = innerFileInfo;
            _cryptoKeyName = new Lazy<CryptoKeyName>(() => UnpackKeyName(keyNameFileInfo));
        }

        public bool Exists => _innerFileInfo.Exists && _innerFileInfo.Name.EndsWith(".encrypted");

        public long Length => CreateReadStream().Length;

        public string PhysicalPath => null;

        public string Name => _innerFileInfo.Name;

        public DateTimeOffset LastModified => _innerFileInfo.LastModified;

        public bool IsDirectory => _innerFileInfo.IsDirectory;

        public Stream CreateReadStream()
        {
            if (!Exists)
            {
                throw new FileNotFoundException(_innerFileInfo.Name);
            }

            DecryptResponse response;
            using (Stream stream = _innerFileInfo.CreateReadStream())
            {
                response = _kms.Decrypt(_cryptoKeyName.Value, ByteString.FromStream(stream));
            }

            MemoryStream memStream = new MemoryStream();
            response.Plaintext.WriteTo(memStream);
            memStream.Seek(0, SeekOrigin.Begin);
            return memStream;
        }

        private static CryptoKeyName UnpackKeyName(IFileInfo keyNameFileInfo)
        {
            if (keyNameFileInfo == null || !keyNameFileInfo.Exists || keyNameFileInfo.IsDirectory)
            {
                throw new FileNotFoundException("Encrypted file found, but failed to find corresponding keyName file.", keyNameFileInfo?.Name);
            }

            using (StreamReader reader = new StreamReader(keyNameFileInfo.CreateReadStream()))
            {
                string line = "";
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
                    {
                        continue;
                    }

                    CryptoKeyName keyName = CryptoKeyName.Parse(line);
                    if (keyName != null)
                    {
                        return keyName;
                    }

                    break;
                }

                throw new Exception(
                    $@"Incorrectly formatted keyName file {keyNameFileInfo.Name}.
                    Expected projects/<projectId>/locations/<locationId>/keyRings/<keyringId>/cryptoKeys/<keyId>
                    Instead, found {line}.");
            }
        }
    }
}