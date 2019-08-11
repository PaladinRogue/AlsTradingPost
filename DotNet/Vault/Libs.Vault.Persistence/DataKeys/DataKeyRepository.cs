using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaladinRogue.Libray.Core.Common.Settings;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Core.Domain.Persistence;
using PaladinRogue.Libray.Vault.Domain.Applications;
using PaladinRogue.Libray.Vault.Domain.Applications.AddDataKey;
using PaladinRogue.Libray.Vault.Domain.Applications.Create;
using PaladinRogue.Libray.Vault.Domain.DataKeys.Persistence;
using PaladinRogue.Libray.Vault.Domain.SharedDataKeys;

namespace PaladinRogue.Libray.Vault.Persistence.DataKeys
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

        public async Task<IEnumerable<DataKey>> GetAllSharedAsync()
        {
            return (await _sharedDataKeyQueryRepository.GetAsync()).Select(s => new DataKey
            {
                Name = s.Name,
                Value = s.Value
            });
        }

        public async Task<DataKey> GetSharedAsync(string name)
        {
            SharedDataKey sharedDataKey = await _sharedDataKeyQueryRepository.GetSingleAsync(s => s.Name == name);

            if (sharedDataKey != null)
            {
                return new DataKey
                {
                    Name = sharedDataKey.Name,
                    Value = sharedDataKey.Value
                };
            }

            return null;
        }

        public async Task<IEnumerable<DataKey>> GetAllAsync()
        {
            Application application = await _applicationQueryRepository.GetSingleAsync(a => a.SystemName == _appSettings.SystemName);

            return application?.ApplicationDataKeys.Select(a => new DataKey
            {
                Name = a.Name,
                Value = a.Value
            }) ?? Enumerable.Empty<DataKey>();
        }

        public async Task<DataKey> GetAsync(string name)
        {
            Application application = await _applicationQueryRepository.GetSingleAsync(a => a.SystemName == _appSettings.SystemName);

            ApplicationDataKey applicationDataKey = application?.ApplicationDataKeys.SingleOrDefault(a => a.Name == name);
            if (applicationDataKey != null)
            {
                return new DataKey
                {
                    Name = applicationDataKey.Name,
                    Value = applicationDataKey.Value
                };
            }

            return null;
        }

        public async Task CreateKeyAsync(DataKey dataKey)
        {
            Application application = await _applicationCommandRepository.GetSingleAsync(a => a.SystemName == _appSettings.SystemName);

            if (application == null)
            {
                application = await _createApplicationCommand.ExecuteAsync(new CreateApplicationCommandDdto
                {
                    SystemName = _appSettings.SystemName
                });

                await _addApplicationDataKeyCommand.ExecuteAsync(application, new AddApplicationDataKeyCommandDdto
                {
                    Type = dataKey.Name,
                    Value = dataKey.Value
                });

                await _applicationCommandRepository.AddAsync(application);
            }
            else
            {
                await _addApplicationDataKeyCommand.ExecuteAsync(application, new AddApplicationDataKeyCommandDdto
                {
                    Type = dataKey.Name,
                    Value = dataKey.Value
                });

                await _applicationCommandRepository.UpdateAsync(application);
            }
        }
    }
}