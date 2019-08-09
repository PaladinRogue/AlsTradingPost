using System;
using System.Threading.Tasks;
using Authentication.Application.Identities.Claims;
using Messaging.Messages;
using Messaging.Setup.Infrastructure.Handlers;
using Messaging.Setup.Infrastructure.MessageBus;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Handlers
{
    public class AddAuthorisationClaimMessageHandler : MessageHandler<AddAuthorisationClaimMessage, AddAuthorisationClaimMessageHandler>
    {
        private readonly IIdentityClaimsApplicationKernalService _identityClaimsApplicationKernalService;

        private readonly ILogger<AddAuthorisationClaimMessageHandler> _logger;

        public AddAuthorisationClaimMessageHandler(
            IMessageBus messageBus,
            IIdentityClaimsApplicationKernalService identityClaimsApplicationKernalService,
            ILogger<AddAuthorisationClaimMessageHandler> logger) : base(messageBus)
        {
            _logger = logger;
            _identityClaimsApplicationKernalService = identityClaimsApplicationKernalService;
        }

        public override async Task ExecuteAsync(AddAuthorisationClaimMessage message)
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