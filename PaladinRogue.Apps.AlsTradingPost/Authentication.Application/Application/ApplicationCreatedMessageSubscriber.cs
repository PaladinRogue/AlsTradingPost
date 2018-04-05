using Common.Messaging.Message;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Application
{
    public class ApplicationCreatedMessageSubscriber : MessageSubscriber<ApplicationCreatedMessage, ApplicationCreatedMessageSubscriber>
    {
        private readonly ILogger<ApplicationCreatedMessageSubscriber> _logger;

        public ApplicationCreatedMessageSubscriber(ILogger<ApplicationCreatedMessageSubscriber> logger, IMessageBus messageBus) : base(messageBus)
        {
            _logger = logger;
        }
        
        public override void Handle(ApplicationCreatedMessage message)
        {
            _logger.LogInformation("hi Dan I am here");
        }
    }
}