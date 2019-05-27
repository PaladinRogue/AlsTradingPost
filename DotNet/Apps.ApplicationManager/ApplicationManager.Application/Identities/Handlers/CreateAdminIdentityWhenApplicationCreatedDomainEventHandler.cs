using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.Domain.Applications.Events;
using Common.Application.Exceptions;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.Logging;

namespace ApplicationManager.ApplicationServices.Identities.Handlers
{
    public class CreateAdminIdentityWhenApplicationCreatedDomainEventHandler : IDomainEventHandler<ApplicationCreatedDomainEvent>
    {
        private readonly ILogger<CreateAdminIdentityWhenApplicationCreatedDomainEventHandler> _logger;

        private readonly ICreateAdminAuthenticationIdentityKernalService _createAdminAuthenticationIdentityKernalService;

        public CreateAdminIdentityWhenApplicationCreatedDomainEventHandler(
            ILogger<CreateAdminIdentityWhenApplicationCreatedDomainEventHandler> logger,
            ICreateAdminAuthenticationIdentityKernalService createAdminAuthenticationIdentityKernalService)
        {
            _logger = logger;
            _createAdminAuthenticationIdentityKernalService = createAdminAuthenticationIdentityKernalService;
        }

        public void Handle(ApplicationCreatedDomainEvent domainEvent)
        {
            try
            {
                _createAdminAuthenticationIdentityKernalService.Create();
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to create admin identity", domainEvent);
            }
        }
    }
}