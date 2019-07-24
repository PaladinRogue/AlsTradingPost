using System;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Gateway.ApplicationServices.Applications.Register;
using Microsoft.Extensions.Logging;

namespace Gateway.ApplicationServices.Subscribers
{
    public class RegisterApplicationMessageSubscriber : MessageSubscriber<RegisterApplicationMessage, RegisterApplicationMessageSubscriber>
    {
        private readonly ILogger<RegisterApplicationMessageSubscriber> _logger;

        private readonly IRegisterApplicationKernalService _registerApplicationKernalService;

        public RegisterApplicationMessageSubscriber(
            IMessageBus messageBus,
            ILogger<RegisterApplicationMessageSubscriber> logger,
            IRegisterApplicationKernalService registerApplicationKernalService) : base(messageBus)
        {
            _logger = logger;
            _registerApplicationKernalService = registerApplicationKernalService;
        }

        public override async Task HandleAsync(RegisterApplicationMessage message)
        {
            try
            {
                await _registerApplicationKernalService.RegisterAsync( new RegisterApplicationAdto
                {
                    Name = message.Name,
                    SystemName = message.SystemName,
                    HostUri = message.HostUri
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to register application");
            }
        }
    }
}