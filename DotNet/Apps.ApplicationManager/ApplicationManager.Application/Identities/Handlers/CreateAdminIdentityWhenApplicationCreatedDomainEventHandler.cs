using ApplicationManager.ApplicationServices.Identities.Interfaces;
using ApplicationManager.ApplicationServices.Identities.Models;
using ApplicationManager.ApplicationServices.Identities.Settings;
using ApplicationManager.Domain.Applications.Events;
using Common.Application.Exceptions;
using Common.Domain.DomainEvents.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationManager.ApplicationServices.Identities.Handlers
{
    public class CreateAdminIdentityWhenApplicationCreatedDomainEventHandler : IDomainEventHandler<ApplicationCreatedDomainEvent>
    {
        private readonly ILogger<CreateAdminIdentityWhenApplicationCreatedDomainEventHandler> _logger;

        private readonly ICreateAdminAuthenticationIdentityKernalService _createAdminAuthenticationIdentityKernalService;

        private readonly SystemAdminIdentitySettings _systemAdminIdentitySettings;

        public CreateAdminIdentityWhenApplicationCreatedDomainEventHandler(
            ILogger<CreateAdminIdentityWhenApplicationCreatedDomainEventHandler> logger,
            IOptions<SystemAdminIdentitySettings> systemAdminIdentitySettingsAccessor,
            ICreateAdminAuthenticationIdentityKernalService createAdminAuthenticationIdentityKernalService)
        {
            _logger = logger;
            _createAdminAuthenticationIdentityKernalService = createAdminAuthenticationIdentityKernalService;
            _systemAdminIdentitySettings = systemAdminIdentitySettingsAccessor.Value;
        }

        public void Handle(ApplicationCreatedDomainEvent domainEvent)
        {
            try
            {
                _createAdminAuthenticationIdentityKernalService.Create(new CreateAdminAuthenticationIdentityAdto
                {
                    EmailAddress = _systemAdminIdentitySettings.Email
                });
            }
            catch (BusinessApplicationException e)
            {
                _logger.LogCritical(e, "Unable to create admin identity", domainEvent);
            }
        }
    }
}