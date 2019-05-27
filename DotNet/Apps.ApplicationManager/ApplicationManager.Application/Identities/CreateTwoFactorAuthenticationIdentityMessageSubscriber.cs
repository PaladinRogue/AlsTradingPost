using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Models;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Messaging.Message;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Identities
{
    public class CreateTwoFactorAuthenticationIdentityMessageSubscriber : MessageSubscriber<CreateTwoFactorAuthenticationIdentityMessage, CreateTwoFactorAuthenticationIdentityMessageSubscriber>
    {
        private readonly ILogger<CreateTwoFactorAuthenticationIdentityMessageSubscriber> _logger;

        private readonly IMapper _mapper;

        private readonly ITwoFactorAuthenticationIdentityKernalService _twoFactorAuthenticationIdentityKernalService;

        public CreateTwoFactorAuthenticationIdentityMessageSubscriber(
            IMessageBus messageBus,
            ILogger<CreateTwoFactorAuthenticationIdentityMessageSubscriber> logger,
            ITwoFactorAuthenticationIdentityKernalService twoFactorAuthenticationIdentityKernalService,
            IMapper mapper) : base(messageBus)
        {
            _logger = logger;
            _twoFactorAuthenticationIdentityKernalService = twoFactorAuthenticationIdentityKernalService;
            _mapper = mapper;
        }

        public override void Handle(CreateTwoFactorAuthenticationIdentityMessage message)
        {
            try
            {
                _twoFactorAuthenticationIdentityKernalService.Create(_mapper.Map<CreateTwoFactorAuthenticationIdentityMessage, CreateTwoFactorAuthenticationIdentityAdto>(message));
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to create two factor authentication identity", message);
            }
        }
    }
}