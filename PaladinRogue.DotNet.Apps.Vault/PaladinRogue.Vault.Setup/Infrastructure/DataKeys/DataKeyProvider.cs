using System;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Core.Domain.Persistence;
using PaladinRogue.Library.Vault.Domain;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys;
using DataKey = PaladinRogue.Library.Core.Domain.DataProtectors.DataKey;

namespace PaladinRogue.Vault.Setup.Infrastructure.DataKeys
{
    public class DataKeyProvider : IDataKeyProvider
    {
        private readonly IQueryRepository<SharedDataKey> _queryRepository;

        private readonly ITransactionManager _transactionManager;

        private readonly IMasterKeyProvider _masterKeyProvider;

        public DataKeyProvider(
            IQueryRepository<SharedDataKey> queryRepository,
            ITransactionManager transactionManager,
            IMasterKeyProvider masterKeyProvider)
        {
            _queryRepository = queryRepository;
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

                    SharedDataKey sharedDataKey = await _queryRepository.GetSingleAsync(s => s.Name == name);

                    if (sharedDataKey == null)
                    {
                        throw new DataKeyNotFoundException(name);
                    }

                    transaction.Commit();

                    return new DataKey
                    {
                        Name = sharedDataKey.Name,
                        Value = sharedDataKey.Value
                    };
                }
                catch (Exception e)
                {
                    throw new DataKeyNotFoundException(name, e);
                }
            }
        }
    }
}