using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.ApplicationServices.Models;
using AutoMapper;
using Common.Application.Transactions;
using Common.Domain.Exceptions;
using Common.Messaging.Message;
using Common.Messaging.Message.Interfaces;
using Common.Messaging.Messages;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Application
{
    public class
        ApplicationCreatedMessageSubscriber : MessageSubscriber<ApplicationCreatedMessage,
            ApplicationCreatedMessageSubscriber>
    {
        private readonly ILogger<ApplicationCreatedMessageSubscriber> _logger;
        private readonly ITransactionManager _transactionManager;
        private readonly IApplicationCommandService _applicationCommandService;
        private readonly IApplicationQueryService _applicationQueryService;

        public ApplicationCreatedMessageSubscriber(ILogger<ApplicationCreatedMessageSubscriber> logger,
            IMessageBus messageBus,
            ITransactionManager transactionManager,
            IApplicationCommandService applicationCommandService,
            IApplicationQueryService applicationQueryService) : base(messageBus)
        {
            _logger = logger;
            _transactionManager = transactionManager;
            _applicationCommandService = applicationCommandService;
            _applicationQueryService = applicationQueryService;
        }

        public override void Handle(ApplicationCreatedMessage message)
        {
            try
            {
                using (ITransaction transaction = _transactionManager.Create())
                {
                    ApplicationProjection applicationProjection = _applicationQueryService.GetByName(message.Name);
                    if (applicationProjection == null)
                    {
                        _applicationCommandService.Create(
                            Mapper.Map<ApplicationCreatedMessage, CreateApplicationDdto>(message));
                    }
                    else
                    {
                        ApplicationProjection applicationProjectionUpdate = Mapper.Map(message, applicationProjection);

                        _applicationCommandService.Update(
                            Mapper.Map<ApplicationProjection, UpdateApplicationDdto>(applicationProjectionUpdate));
                    }

                    transaction.Commit();
                }
            }
            catch (DomainException e)
            {
                _logger.LogCritical(e, "Unable to create or update application");
            }
        }
    }
}