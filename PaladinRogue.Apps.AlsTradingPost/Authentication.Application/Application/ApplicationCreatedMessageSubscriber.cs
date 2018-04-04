using Common.Messaging;
using Common.Messaging.Interfaces;
using Message.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Application
{
    public class ApplicationCreatedMessageSubscriber : IMessageSubscriber, IMessageSubscriber<ApplicationCreatedMessage>
    {
        private readonly ILogger<ApplicationCreatedMessageSubscriber> _logger;

        public ApplicationCreatedMessageSubscriber(ILogger<ApplicationCreatedMessageSubscriber> logger)
        {
            _logger = logger;
        }

        public void Subscribe()
        {
            MessageSubscriberFactory.Register<ApplicationCreatedMessage>(Handle);
        }

        public void Handle(ApplicationCreatedMessage message)
        {
            _logger.LogInformation("hi Dan I am here");
        }
    }
}