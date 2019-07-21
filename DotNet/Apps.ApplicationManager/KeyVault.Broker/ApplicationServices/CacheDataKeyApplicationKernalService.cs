using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApplicationServices.Transactions;
using Common.Resources.Encryption;
using KeyVault.Broker.Caching;
using KeyVault.Broker.Domain;
using KeyVault.Broker.Domain.Persistence;
using KeyVault.Domain.SharedDataKeys;

namespace KeyVault.Broker.ApplicationServices
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

        public async Task CreateAndCacheAllAsync<T>() where T : Enum
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                IList<DataKey<T>> dataKeys = (await _dataKeyRepository.GetAllAsync<T>()).ToList();

                foreach (T type in Enum.GetValues(typeof(T)))
                {
                    DataKey<T> dataKey = dataKeys.SingleOrDefault(k => k.Type.Equals(type));

                    if (dataKey == null)
                    {
                        dataKey = new DataKey<T>
                        {
                            Type = type,
                            Value = _encryptionFactory.CreateKey()
                        };

                        await _dataKeyRepository.CreateKeyAsync(dataKey);
                    }

                    await _cacheService.AddAsync(new DataKeyCacheKey<T>(type), dataKey);
                }


                IList<DataKey<SharedDataKeyType>> sharedDataKeys = (await _dataKeyRepository.GetAllSharedAsync()).ToList();

                foreach (DataKey<SharedDataKeyType> sharedDataKey in sharedDataKeys)
                {
                    await _cacheService.AddAsync(new DataKeyCacheKey<SharedDataKeyType>(sharedDataKey.Type), sharedDataKey);
                }

                transaction.Commit();
            }
        }
    }
}