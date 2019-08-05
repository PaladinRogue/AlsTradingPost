using System;
using System.Threading.Tasks;
using Authentication.ApplicationServices.Users.CreateAdmin;
using Authentication.ApplicationServices.Users.Models;
using AutoMapper;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Messages;
using Common.Resources.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Authentication.ApplicationServices.Handlers
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
            IMapper mapper, IOptions<AppSettings> appSettingsAccessor) : base(messageBus)
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