using System;
using System.Threading.Tasks;
using ApplicationManager.ApplicationServices.Identities.CreateAdmin;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Subscribers
{
    public class CreateAdminIdentityMessageSubscriber : MessageSubscriber<CreateAdminIdentityMessage, CreateAdminIdentityMessageSubscriber>
    {
        private readonly ILogger<CreateAdminIdentityMessageSubscriber> _logger;

        private readonly ICreateAdminAuthenticationIdentityKernalService _createAdminAuthenticationIdentityKernalService;

        public CreateAdminIdentityMessageSubscriber(
            IMessageBus messageBus,
            ILogger<CreateAdminIdentityMessageSubscriber> logger,
            ICreateAdminAuthenticationIdentityKernalService createAdminAuthenticationIdentityKernalService) : base(messageBus)
        {
            _logger = logger;
            _createAdminAuthenticationIdentityKernalService = createAdminAuthenticationIdentityKernalService;
        }

        public override async Task HandleAsync(CreateAdminIdentityMessage message)
        {
            try
            {
                await _createAdminAuthenticationIdentityKernalService.CreateAsync(new CreateAdminAuthenticationIdentityAdto
                {
                    EmailAddress = message.EmailAddress,
                    ApplicationName = message.ApplicationName
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin user");
            }
        }
    }
}