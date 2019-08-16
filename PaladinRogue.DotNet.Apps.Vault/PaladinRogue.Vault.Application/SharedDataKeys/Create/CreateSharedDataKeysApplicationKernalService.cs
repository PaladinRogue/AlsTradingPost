using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaladinRogue.Library.Core.Application.Transactions;
using PaladinRogue.Library.Core.Common.Encryption;
using PaladinRogue.Library.Core.Domain.Persistence;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys.Change;
using PaladinRogue.Library.Vault.Domain.SharedDataKeys.Create;

namespace PaladinRogue.Vault.Application.SharedDataKeys.Create
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

                    foreach (FieldInfo fieldInfo in typeof(Library.Core.Domain.DataProtectors.SharedDataKeys).GetFields())
                    {
                        string keyName = fieldInfo.GetRawConstantValue().ToString();

                        SharedDataKey key = existingKeys.SingleOrDefault(k => k.Name == keyName);
                        if (key == null)
                        {
                            SharedDataKey sharedDataKey = await _createSharedDataKeyCommand.ExecuteAsync(new CreateSharedDataKeyCommandDdto
                            {
                                Name = keyName,
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