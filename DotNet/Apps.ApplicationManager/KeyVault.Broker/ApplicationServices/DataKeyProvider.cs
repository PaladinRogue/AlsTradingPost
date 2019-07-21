using System;
using System.Threading.Tasks;
using Common.ApplicationServices.Exceptions;
using Common.ApplicationServices.Transactions;
using KeyVault.Broker.Domain;
using KeyVault.Broker.Domain.Persistence;
using KeyVault.Domain.SharedDataKeys;

namespace KeyVault.Broker.ApplicationServices
{
    public class DataKeyProvider : IDataKeyProvider
    {
        private readonly IDataKeyRepository _dataKeyRepository;

        private readonly ITransactionManager _transactionManager;

        public DataKeyProvider(IDataKeyRepository dataKeyRepository, ITransactionManager transactionManager)
        {
            _dataKeyRepository = dataKeyRepository;
            _transactionManager = transactionManager;
        }

        public async Task<DataKey<T>> GetAsync<T>(T type) where T : Enum
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    DataKey<T> dataKey;

                    if (typeof(T) == typeof(SharedDataKeyType))
                    {
                        dataKey = await _dataKeyRepository.GetSharedAsync((SharedDataKeyType) (object) type) as DataKey<T>;
                    }
                    else
                    {
                        dataKey = await _dataKeyRepository.GetAsync(type);
                    }

                    if (dataKey == null)
                    {
                        throw new BusinessApplicationException(ExceptionType.Unknown, $"Data key not available for type: {type}");
                    }

                    transaction.Commit();

                    return dataKey;
                }
                catch (Exception e)
                {
                    throw new BusinessApplicationException(ExceptionType.Unknown, $"Data key not available for type: {type}", e);
                }
            }
        }
    }
}