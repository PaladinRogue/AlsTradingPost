using ApplicationManager.ApplicationServices.Users.CreateAdmin;
using ApplicationManager.ApplicationServices.Users.Models;
using AutoMapper;
using Common.ApplicationServices.Exceptions;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Common.Setup.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationManager.ApplicationServices.Subscribers
{
    public class AdminIdentityCreatedMessageSubscriber : MessageSubscriber<AdminIdentityCreatedMessage, AdminIdentityCreatedMessageSubscriber>
    {
        private readonly ILogger<AdminIdentityCreatedMessageSubscriber> _logger;

        private readonly IMapper _mapper;

        private readonly ICreateAdminUserApplicationKernalService _createAdminUserApplicationKernalService;

        private readonly AppSettings _appSettings;

        public AdminIdentityCreatedMessageSubscriber(
            IMessageBus messageBus,
            ILogger<AdminIdentityCreatedMessageSubscriber> logger,
            ICreateAdminUserApplicationKernalService createAdminUserApplicationKernalService,
            IMapper mapper, IOptions<AppSettings> appSettingsAccessor) : base(messageBus)
        {
            _logger = logger;
            _createAdminUserApplicationKernalService = createAdminUserApplicationKernalService;
            _mapper = mapper;
            _appSettings = appSettingsAccessor.Value;
        }

        public override void Handle(AdminIdentityCreatedMessage message)
        {
            try
            {
                if (_appSettings.SystemName == message.ApplicationName)
                {
                    _createAdminUserApplicationKernalService.Create(_mapper.Map<AdminIdentityCreatedMessage, CreateUserAdto>(message));
                }
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to create admin user");
            }
        }
    }
}