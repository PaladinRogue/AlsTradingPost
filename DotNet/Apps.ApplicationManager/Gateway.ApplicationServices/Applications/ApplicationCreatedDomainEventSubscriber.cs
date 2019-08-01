using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using Gateway.Domain.Applications.Events;

namespace Gateway.ApplicationServices.Applications
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