using System;
using System.Threading.Tasks;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Messages;
using Gateway.ApplicationServices.Applications.Register;
using Microsoft.Extensions.Logging;

namespace Gateway.ApplicationServices.Handlers
{
    public class RegisterApplicationMessageHandler : MessageHandler<RegisterApplicationMessage, RegisterApplicationMessageHandler>
    {
        private readonly ILogger<RegisterApplicationMessageHandler> _logger;

        private readonly IRegisterApplicationKernalService _registerApplicationKernalService;

        public RegisterApplicationMessageHandler(
            IMessageBus messageBus,
            ILogger<RegisterApplicationMessageHandler> logger,
            IRegisterApplicationKernalService registerApplicationKernalService) : base(messageBus)
        {
            _logger = logger;
            _registerApplicationKernalService = registerApplicationKernalService;
        }

        public override async Task ExecuteAsync(RegisterApplicationMessage message)
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