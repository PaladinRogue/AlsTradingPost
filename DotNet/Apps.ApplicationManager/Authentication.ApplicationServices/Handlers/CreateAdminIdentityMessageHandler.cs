using System;
using System.Threading.Tasks;
using Authentication.ApplicationServices.Identities.CreateAdmin;
using Common.Messaging.Infrastructure.Handlers;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.ApplicationServices.Handlers
{
    public class CreateAdminIdentityMessageHandler : MessageHandler<CreateAdminIdentityMessage, CreateAdminIdentityMessageHandler>
    {
        private readonly ILogger<CreateAdminIdentityMessageHandler> _logger;

        private readonly ICreateAdminAuthenticationIdentityKernalService _createAdminAuthenticationIdentityKernalService;

        public CreateAdminIdentityMessageHandler(
            IMessageBus messageBus,
            ILogger<CreateAdminIdentityMessageHandler> logger,
            ICreateAdminAuthenticationIdentityKernalService createAdminAuthenticationIdentityKernalService) : base(messageBus)
        {
            _logger = logger;
            _createAdminAuthenticationIdentityKernalService = createAdminAuthenticationIdentityKernalService;
        }

        public override async Task ExecuteAsync(CreateAdminIdentityMessage message)
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