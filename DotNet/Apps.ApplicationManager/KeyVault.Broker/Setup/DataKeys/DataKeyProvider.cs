using System;
using System.Threading.Tasks;
using Common.ApplicationServices.Transactions;
using Common.Domain.DataProtectors;
using KeyVault.Broker.Domain.Persistence;
using KeyVault.Broker.Persistence;
using KeyVault.Domain;
using DataKey = Common.Domain.DataProtectors.DataKey;

namespace KeyVault.Broker.Setup.DataKeys
{
    public class DataKeyProvider : IDataKeyProvider
    {
        private readonly IDataKeyRepository _dataKeyRepository;

        private readonly IKeyVaultTransactionManager _transactionManager;

        private readonly IMasterKeyProvider _masterKeyProvider;

        public DataKeyProvider(
            IDataKeyRepository dataKeyRepository,
            IKeyVaultTransactionManager transactionManager,
            IMasterKeyProvider masterKeyProvider)
        {
            _dataKeyRepository = dataKeyRepository;
            _transactionManager = transactionManager;
            _masterKeyProvider = masterKeyProvider;
        }

        public async Task<DataKey> GetAsync(string name)
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    if (name == MasterDataKeys.Master)
                    {
                        return new DataKey
                        {
                            Name = MasterDataKeys.Master,
                            Value = (await _masterKeyProvider.GetAsync()).Value
                        };
                    }

                    DataKey dataKey = await _dataKeyRepository.GetSharedAsync(name) ?? await _dataKeyRepository.GetAsync(name);

                    if (dataKey == null)
                    {
                        throw new DataKeyNotFoundException(name);
                    }

                    transaction.Commit();

                    return dataKey;
                }
                catch (Exception e)
                {
                    throw new DataKeyNotFoundException(name, e);
                }
            }
        }
    }
}