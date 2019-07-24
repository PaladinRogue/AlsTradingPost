using System;
using System.Threading.Tasks;
using Authentication.ApplicationServices.Identities.Claims;
using Common.Messaging.Infrastructure.MessageBus;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.ApplicationServices.Subscribers
{
    public class AddAuthorisationClaimMessageSubscriber : MessageSubscriber<AddAuthorisationClaimMessage, AddAuthorisationClaimMessageSubscriber>
    {
        private readonly IIdentityClaimsApplicationKernalService _identityClaimsApplicationKernalService;

        private readonly ILogger<AddAuthorisationClaimMessageSubscriber> _logger;

        public AddAuthorisationClaimMessageSubscriber(
            IMessageBus messageBus,
            IIdentityClaimsApplicationKernalService identityClaimsApplicationKernalService,
            ILogger<AddAuthorisationClaimMessageSubscriber> logger) : base(messageBus)
        {
            _logger = logger;
            _identityClaimsApplicationKernalService = identityClaimsApplicationKernalService;
        }

        public override async Task HandleAsync(AddAuthorisationClaimMessage message)
        {
            try
            {
                await _identityClaimsApplicationKernalService.UpdateAsync(new UpdateIdentityClaimAdto
                {
                    IdentityId = message.IdentityId,
                    Type = message.ClaimType,
                    Value = message.Value
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable add claim");
            }
        }
    }
}