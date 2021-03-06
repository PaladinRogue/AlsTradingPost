using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PaladinRogue.Authentication.Application.Identities.CreateAdmin;
using PaladinRogue.Authentication.Messages;
using PaladinRogue.Library.Messaging.Common.Handlers;
using PaladinRogue.Library.Messaging.Common.MessageBus;

namespace PaladinRogue.Authentication.Application.Handlers
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