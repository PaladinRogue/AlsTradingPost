using ApplicationManager.ApplicationServices.Applications.Interfaces;
using ApplicationManager.ApplicationServices.Applications.Models;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Messaging.Message;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Applications
{
    public class RegisterApplicationMessageSubscriber : MessageSubscriber<CreateSystemAdminIdentityMessage, RegisterApplicationMessageSubscriber>
    {
        private readonly ILogger<RegisterApplicationMessageSubscriber> _logger;

        private readonly IMapper _mapper;

        private readonly IRegisterApplicationKernalService _registerApplicationKernalService;

        public RegisterApplicationMessageSubscriber(
            IMessageBus messageBus,
            ILogger<RegisterApplicationMessageSubscriber> logger,
            IRegisterApplicationKernalService registerApplicationKernalService,
            IMapper mapper) : base(messageBus)
        {
            _logger = logger;
            _registerApplicationKernalService = registerApplicationKernalService;
            _mapper = mapper;
        }

        public override void Handle(CreateSystemAdminIdentityMessage message)
        {
            try
            {
                _registerApplicationKernalService.Register(_mapper.Map<CreateSystemAdminIdentityMessage, RegisterApplicationAdto>(message));
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to register application");
            }
        }
    }
}