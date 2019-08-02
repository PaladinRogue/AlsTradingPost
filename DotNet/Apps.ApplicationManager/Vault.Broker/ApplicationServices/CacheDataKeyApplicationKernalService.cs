using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using Common.Domain.DataProtectors;
using Common.Resources.Encryption;
using Vault.Broker.Caching;
using Vault.Broker.Domain.Persistence;
using Vault.Broker.Setup.DataKeys;

namespace Vault.Broker.ApplicationServices
{
    public class CacheDataKeyApplicationKernalService : ICacheDataKeyApplicationKernalService
    {
        private readonly ApplicationCacheService _cacheService;

        private readonly IDataKeyRepository _dataKeyRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly IEncryptionFactory _encryptionFactory;

        public CacheDataKeyApplicationKernalService(
            ApplicationCacheService cacheService,
            IDataKeyRepository dataKeyRepository,
            ITransactionManager transactionManager,
            IEncryptionFactory encryptionFactory)
        {
            _cacheService = cacheService;
            _dataKeyRepository = dataKeyRepository;
            _transactionManager = transactionManager;
            _encryptionFactory = encryptionFactory;
        }

        public async Task CreateAndCacheAllAsync<T>()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IList<DataKey> dataKeys = (await _dataKeyRepository.GetAllAsync()).ToList();

                if (dataKeys.GroupBy(k => k.Name).Any(g => g.Count() > 1))
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, "Duplicate data key detected");
                }

                foreach (FieldInfo field in typeof(T).GetFields())
                {
                    string keyName = field.GetRawConstantValue().ToString();

                    DataKey dataKey = dataKeys.SingleOrDefault(k => k.Name == keyName);

                    if (dataKey == null)
                    {
                        dataKey = new DataKey
                        {
                            Name = keyName,
                            Value = _encryptionFactory.CreateKey()
                        };

                        await _dataKeyRepository.CreateKeyAsync(dataKey);
                    }

                    await _cacheService.AddAsync(new DataKeyCacheKey(keyName), dataKey);
                }


                IList<DataKey> sharedDataKeys = (await _dataKeyRepository.GetAllSharedAsync()).ToList();

                foreach (DataKey sharedDataKey in sharedDataKeys)
                {
                    await _cacheService.AddAsync(new DataKeyCacheKey(sharedDataKey.Name), sharedDataKey);
                }

                transaction.Commit();
            }
        }
    }
}