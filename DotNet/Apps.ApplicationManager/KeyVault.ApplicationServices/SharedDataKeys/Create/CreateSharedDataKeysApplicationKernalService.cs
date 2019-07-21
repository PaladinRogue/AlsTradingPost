using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApplicationServices.Transactions;
using Common.Domain.Persistence;
using Common.Resources.Encryption;
using KeyVault.Domain.SharedDataKeys;
using KeyVault.Domain.SharedDataKeys.Change;
using KeyVault.Domain.SharedDataKeys.Create;
using Microsoft.Extensions.Logging;

namespace KeyVault.ApplicationServices.SharedDataKeys.Create
{
    public class CreateSharedDataKeysApplicationKernalService : ICreateSharedDataKeysApplicationKernalService
    {
        private readonly ITransactionManager _transactionManager;

        private readonly ILogger<CreateSharedDataKeysApplicationKernalService> _logger;

        private readonly ICommandRepository<SharedDataKey> _commandRepository;

        private readonly ICreateSharedDataKeyCommand _createSharedDataKeyCommand;

        private readonly IChangeSharedDataKeyCommand _changeSharedDataKeyCommand;

        private readonly IEncryptionFactory _encryptionFactory;

        public CreateSharedDataKeysApplicationKernalService(
            ITransactionManager transactionManager,
            ILogger<CreateSharedDataKeysApplicationKernalService> logger,
            ICommandRepository<SharedDataKey> commandRepository,
            ICreateSharedDataKeyCommand createSharedDataKeyCommand,
            IChangeSharedDataKeyCommand changeSharedDataKeyCommand,
            IEncryptionFactory encryptionFactory)
        {
            _transactionManager = transactionManager;
            _logger = logger;
            _commandRepository = commandRepository;
            _createSharedDataKeyCommand = createSharedDataKeyCommand;
            _changeSharedDataKeyCommand = changeSharedDataKeyCommand;
            _encryptionFactory = encryptionFactory;
        }

        public async Task ExecuteAsync()
        {
            using (ITransaction transaction = _transactionManager.Create())
            {
                try
                {
                    IList<SharedDataKey> existingKeys = (await _commandRepository.GetAsync()).ToList();

                    foreach (SharedDataKeyType sharedDataKeyType in Enum.GetValues(typeof(SharedDataKeyType)))
                    {
                        SharedDataKey key = existingKeys.SingleOrDefault(k => k.Type == sharedDataKeyType);
                        if (key == null)
                        {
                            SharedDataKey sharedDataKey = await _createSharedDataKeyCommand.ExecuteAsync(new CreateSharedDataKeyCommandDdto
                            {
                                Type = sharedDataKeyType,
                                Value = _encryptionFactory.CreateKey()
                            });

                            await _commandRepository.AddAsync(sharedDataKey);
                        }
                        else
                        {
                            await _changeSharedDataKeyCommand.ExecuteAsync(key, new ChangeSharedDataKeyCommandDdto
                            {
                                Value = _encryptionFactory.CreateKey()
                            });

                            await _commandRepository.UpdateAsync(key);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    _logger.LogCritical("Unable to initialise shared data keys");
                    throw;
                }
            }
        }
    }
}