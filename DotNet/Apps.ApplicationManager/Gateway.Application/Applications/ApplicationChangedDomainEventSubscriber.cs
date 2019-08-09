using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using Gateway.Domain.Applications.Events;

namespace Gateway.Application.Applications
{
    public class ApplicationChangedDomainEventSubscriber : IDomainEventSubscriber<ApplicationChangedDomainEvent>
    {
        private readonly IApplicationKernalService _applicationKernalService;

        public ApplicationChangedDomainEventSubscriber(IApplicationKernalService applicationKernalService)
        {
            _applicationKernalService = applicationKernalService;
        }

        public Task ExecuteAsync(ApplicationChangedDomainEvent domainEvent)
        {
            return _applicationKernalService.UpdateAsync(domainEvent.Application);
        }
    }
}