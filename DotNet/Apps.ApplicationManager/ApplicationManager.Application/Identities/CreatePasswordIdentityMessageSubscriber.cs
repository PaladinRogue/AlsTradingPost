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
    public class CreatePasswordIdentityMessageSubscriber : MessageSubscriber<CreatePasswordIdentityMessage, CreatePasswordIdentityMessageSubscriber>
    {
        private readonly ILogger<CreatePasswordIdentityMessageSubscriber> _logger;

        private readonly IMapper _mapper;

        private readonly IPasswordIdentityKernalService _passwordIdentityKernalService;

        public CreatePasswordIdentityMessageSubscriber(
            IMessageBus messageBus,
            ILogger<CreatePasswordIdentityMessageSubscriber> logger,
            IPasswordIdentityKernalService passwordIdentityKernalService,
            IMapper mapper) : base(messageBus)
        {
            _logger = logger;
            _passwordIdentityKernalService = passwordIdentityKernalService;
            _mapper = mapper;
        }

        public override void Handle(CreatePasswordIdentityMessage message)
        {
            try
            {
                _passwordIdentityKernalService.Create(_mapper.Map<CreatePasswordIdentityMessage, CreatePasswordIdentityAdto>(message));
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to create password identity");
            }
        }
    }
}