using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Vault.Domain;
using PaladinRogue.Library.Vault.Domain.DataKeys.Persistence;
using PaladinRogue.Library.Vault.Setup.Infrastructure.Transactions;
using DataKey = PaladinRogue.Library.Core.Domain.DataProtectors.DataKey;

namespace PaladinRogue.Library.Vault.Setup.Infrastructure.DataKeys
{
    public class DataKeyProvider : IDataKeyProvider
    {
        private readonly IDataKeyRepository _dataKeyRepository;

        private readonly IVaultTransactionManager _transactionManager;

        private readonly IMasterKeyProvider _masterKeyProvider;

        public DataKeyProvider(
            IDataKeyRepository dataKeyRepository,
            IVaultTransactionManager transactionManager,
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