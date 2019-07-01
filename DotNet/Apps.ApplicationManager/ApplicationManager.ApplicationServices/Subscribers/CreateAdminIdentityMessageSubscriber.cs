using ApplicationManager.ApplicationServices.Users.CreateAdmin;
using ApplicationManager.ApplicationServices.Users.Models;
using AutoMapper;
using Common.ApplicationServices.Exceptions;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Common.Resources.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationManager.ApplicationServices.Subscribers
{
    public class CreateAdminIdentityMessageSubscriber : MessageSubscriber<CreateAdminIdentityMessage, CreateAdminIdentityMessageSubscriber>
    {
        private readonly ILogger<CreateAdminIdentityMessageSubscriber> _logger;

        private readonly IMapper _mapper;

        private readonly ICreateAdminUserApplicationKernalService _createAdminUserApplicationKernalService;

        private readonly AppSettings _appSettings;

        public CreateAdminIdentityMessageSubscriber(
            IMessageBus messageBus,
            ILogger<CreateAdminIdentityMessageSubscriber> logger,
            ICreateAdminUserApplicationKernalService createAdminUserApplicationKernalService,
            IMapper mapper, IOptions<AppSettings> appSettingsAccessor) : base(messageBus)
        {
            _logger = logger;
            _createAdminUserApplicationKernalService = createAdminUserApplicationKernalService;
            _mapper = mapper;
            _appSettings = appSettingsAccessor.Value;
        }

        public override void Handle(CreateAdminIdentityMessage message)
        {
            try
            {
                if (_appSettings.SystemName == message.ApplicationName)
                {
                    _createAdminUserApplicationKernalService.Create(_mapper.Map<CreateAdminIdentityMessage, CreateUserAdto>(message));
                }
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to create admin user");
            }
        }
    }
}