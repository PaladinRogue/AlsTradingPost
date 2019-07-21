using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Domain.Persistence;
using Common.Resources.Extensions;
using Common.Resources.Settings;
using KeyVault.Broker.Domain;
using KeyVault.Broker.Domain.Persistence;
using KeyVault.Domain.Applications;
using KeyVault.Domain.Applications.AddDataKey;
using KeyVault.Domain.Applications.Create;
using KeyVault.Domain.SharedDataKeys;
using Microsoft.Extensions.Options;

namespace KeyVault.Broker.Persistence
{
    public class DataKeyRepository : IDataKeyRepository
    {
        private readonly ICommandRepository<Application> _applicationCommandRepository;

        private readonly IQueryRepository<Application> _applicationQueryRepository;

        private readonly IQueryRepository<SharedDataKey> _sharedDataKeyQueryRepository;

        private readonly AppSettings _appSettings;

        private readonly ICreateApplicationCommand _createApplicationCommand;

        private readonly IAddApplicationDataKeyCommand _addApplicationDataKeyCommand;

        public DataKeyRepository(
            ICommandRepository<Application> applicationCommandRepository,
            IQueryRepository<Application> applicationQueryRepository,
            IQueryRepository<SharedDataKey> sharedDataKeyQueryRepository,
            IOptions<AppSettings> appSettingsAccessor,
            ICreateApplicationCommand createApplicationCommand,
            IAddApplicationDataKeyCommand addApplicationDataKeyCommand)
        {
            _applicationCommandRepository = applicationCommandRepository;
            _applicationQueryRepository = applicationQueryRepository;
            _sharedDataKeyQueryRepository = sharedDataKeyQueryRepository;
            _createApplicationCommand = createApplicationCommand;
            _addApplicationDataKeyCommand = addApplicationDataKeyCommand;
            _appSettings = appSettingsAccessor.Value;
        }

        public async Task<IEnumerable<DataKey<SharedDataKeyType>>> GetAllSharedAsync()
        {
            return (await _sharedDataKeyQueryRepository.GetAsync()).Select(s => new DataKey<SharedDataKeyType>
            {
                Type = s.Type,
                Value = s.Value
            });
        }

        public async Task<DataKey<SharedDataKeyType>> GetSharedAsync(SharedDataKeyType type)
        {
            SharedDataKey sharedDataKey = await _sharedDataKeyQueryRepository.GetSingleAsync(s => s.Type == type);

            if (sharedDataKey != null)
            {
                return new DataKey<SharedDataKeyType>
                {
                    Type = sharedDataKey.Type,
                    Value = sharedDataKey.Value
                };
            }

            return null;
        }

        public async Task<IEnumerable<DataKey<T>>> GetAllAsync<T>() where T : Enum
        {
            Application application = await _applicationQueryRepository.GetSingleAsync(a => a.SystemName == _appSettings.SystemName);

            return application?.ApplicationDataKeys.Select(a => new DataKey<T>
            {
                Type = a.Type.ToEnum<T>(),
                Value = a.Value
            }) ?? Enumerable.Empty<DataKey<T>>();
        }

        public async Task<DataKey<T>> GetAsync<T>(T type) where T : Enum
        {
            Application application = await _applicationQueryRepository.GetSingleAsync(a => a.SystemName == _appSettings.SystemName);

            ApplicationDataKey applicationDataKey = application?.ApplicationDataKeys.SingleOrDefault(a => a.Type == type.ToInt());
            if (applicationDataKey != null)
            {
                return new DataKey<T>
                {
                    Type = applicationDataKey.Type.ToEnum<T>(),
                    Value = applicationDataKey.Value
                };
            }

            return null;
        }

        public async Task CreateKeyAsync<T>(DataKey<T> dataKey) where T : Enum
        {
            Application application = await _applicationQueryRepository.GetSingleAsync(a => a.SystemName == _appSettings.SystemName);

            if (application == null)
            {
                application = await _createApplicationCommand.ExecuteAsync(new CreateApplicationCommandDdto
                {
                    SystemName = _appSettings.SystemName
                });

                await _applicationCommandRepository.AddAsync(application);
            }
            else
            {
                await _addApplicationDataKeyCommand.ExecuteAsync(application, new AddApplicationDataKeyCommandDdto
                {
                    Type = dataKey.Type.ToInt(),
                    Value = dataKey.Value
                });

                await _applicationCommandRepository.UpdateAsync(application);
            }
        }
    }
}