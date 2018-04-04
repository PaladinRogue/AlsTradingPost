using Common.Messaging.Interfaces;
using Message.Broker.Interfaces;
using Message.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Application
{
    public class ApplicationCreatedMessageSubscriber : IMessageSubscriber, IMessageSubscriber<ApplicationCreatedMessage>
    {
        private readonly ILogger<ApplicationCreatedMessageSubscriber> _logger;
        private readonly IMessageBus _messageBus;

        public ApplicationCreatedMessageSubscriber(ILogger<ApplicationCreatedMessageSubscriber> logger, IMessageBus messageBus)
        {
            _logger = logger;
            _messageBus = messageBus;
        }

        public void Subscribe()
        {
            _messageBus.Subscribe<ApplicationCreatedMessage, ApplicationCreatedMessageSubscriber>(Handle);
        }

        public void Handle(ApplicationCreatedMessage message)
        {
            _logger.LogInformation("hi Dan I am here");
        }
    }
}