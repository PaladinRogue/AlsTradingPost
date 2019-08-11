using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaladinRogue.Authentication.Application.Users.CreateAdmin;
using PaladinRogue.Authentication.Application.Users.Models;
using PaladinRogue.Authentication.Messages;
using PaladinRogue.Libray.Core.Common.Settings;
using PaladinRogue.Libray.Messaging.Common.Handlers;
using PaladinRogue.Libray.Messaging.Common.MessageBus;

namespace PaladinRogue.Authentication.Application.Handlers
{
    public class AdminIdentityCreatedMessageHandler : MessageHandler<AdminIdentityCreatedMessage, AdminIdentityCreatedMessageHandler>
    {
        private readonly ILogger<AdminIdentityCreatedMessageHandler> _logger;

        private readonly IMapper _mapper;

        private readonly ICreateUserApplicationKernalService _createUserApplicationKernalService;

        private readonly AppSettings _appSettings;

        public AdminIdentityCreatedMessageHandler(
            IMessageBus messageBus,
            ILogger<AdminIdentityCreatedMessageHandler> logger,
            ICreateUserApplicationKernalService createUserApplicationKernalService,
            IMapper mapper,
            IOptions<AppSettings> appSettingsAccessor) : base(messageBus)
        {
            _logger = logger;
            _createUserApplicationKernalService = createUserApplicationKernalService;
            _mapper = mapper;
            _appSettings = appSettingsAccessor.Value;
        }

        public override async Task ExecuteAsync(AdminIdentityCreatedMessage message)
        {
            try
            {
                if (_appSettings.SystemName == message.ApplicationName)
                {
                    await _createUserApplicationKernalService.CreateAsync(_mapper.Map<AdminIdentityCreatedMessage, CreateUserAdto>(message));
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin user");
            }
        }
    }
}