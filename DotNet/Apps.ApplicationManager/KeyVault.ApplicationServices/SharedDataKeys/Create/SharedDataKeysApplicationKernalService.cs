using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using Common.Resources.Encryption;
using KeyVault.Domain.SharedDataKeys;
using KeyVault.Domain.SharedDataKeys.Create;
using Microsoft.Extensions.Logging;

namespace KeyVault.ApplicationServices.SharedDataKeys.Create
{
    public class SharedDataKeysApplicationKernalService : ISharedDataKeysApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ILogger<SharedDataKeysApplicationKernalService> _logger;

        private readonly ICommandRepository<SharedDataKey> _commandRepository;

        private readonly ICreateSharedDataKeyCommand _createSharedDataKeyCommand;

        private readonly IEncryptionFactory _encryptionFactory;

        public SharedDataKeysApplicationKernalService(
            ITransactionManager transactionManager,
            ILogger<SharedDataKeysApplicationKernalService> logger,
            ICommandRepository<SharedDataKey> commandRepository,
            ICreateSharedDataKeyCommand createSharedDataKeyCommand,
            IEncryptionFactory encryptionFactory)
        {
            _transactionManager = transactionManager;
            _logger = logger;
            _commandRepository = commandRepository;
            _createSharedDataKeyCommand = createSharedDataKeyCommand;
            _encryptionFactory = encryptionFactory;
        }

        public async Task Initialise()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    IList<SharedDataKey> existingKeys = (await _commandRepository.GetAsync()).ToList();

                    foreach (SharedDatKeyType sharedDataKeyType in Enum.GetValues(typeof(SharedDatKeyType)))
                    {
                        if (existingKeys.Any(k => k.Type == sharedDataKeyType))
                        {
                            continue;
                        }

                        SharedDataKey sharedDataKey = await _createSharedDataKeyCommand.ExecuteAsync(new CreateSharedDataKeyCommandDdto
                        {
                            Type = sharedDataKeyType,
                            Value = _encryptionFactory.CreateKey()
                        });

                        await _commandRepository.AddAsync(sharedDataKey);

                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Unable to initialise shared data keys");
                }
            }
        }
    }
}