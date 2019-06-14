using ApplicationManager.ApplicationServices.Applications.Register;
using AutoMapper;
using Common.Application.Exceptions;
using Common.Messaging.Infrastructure.Interfaces;
using Common.Messaging.Infrastructure.Subscribers;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Subscribers
{
    public class RegisterApplicationMessageSubscriber : MessageSubscriber<RegisterApplicationMessage, RegisterApplicationMessageSubscriber>
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

        public override void Handle(RegisterApplicationMessage message)
        {
            try
            {
                _registerApplicationKernalService.Register(_mapper.Map<RegisterApplicationMessage, RegisterApplicationAdto>(message));
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to register application");
            }
        }
    }
}