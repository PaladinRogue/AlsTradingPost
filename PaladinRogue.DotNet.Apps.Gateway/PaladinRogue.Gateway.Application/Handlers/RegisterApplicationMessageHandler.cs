using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaladinRogue.Gateway.Application.Applications.Register;
using PaladinRogue.Gateway.Messages;
using PaladinRogue.Library.Messaging.Common.Handlers;
using PaladinRogue.Library.Messaging.Common.MessageBus;

namespace PaladinRogue.Gateway.Application.Handlers
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