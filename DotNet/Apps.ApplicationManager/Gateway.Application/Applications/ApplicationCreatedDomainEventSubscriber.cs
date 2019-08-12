using System.Threading.Tasks;
using PaladinRogue.Gateway.Domain.Applications.Events;
using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Gateway.Application.Applications
{
    public class ApplicationCreatedDomainEventSubscriber : IDomainEventSubscriber<ApplicationCreatedDomainEvent>
    {
        private readonly IApplicationKernalService _applicationKernalService;

        public ApplicationCreatedDomainEventSubscriber(IApplicationKernalService applicationKernalService)
        {
            _applicationKernalService = applicationKernalService;
        }

        public Task ExecuteAsync(ApplicationCreatedDomainEvent domainEvent)
        {
            return _applicationKernalService.CreateAsync(domainEvent.Application);
        }
    }
}