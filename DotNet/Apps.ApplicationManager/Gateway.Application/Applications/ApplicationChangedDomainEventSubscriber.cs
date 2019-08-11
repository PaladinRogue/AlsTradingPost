using System.Threading.Tasks;
using PaladinRogue.Gateway.Domain.Applications.Events;
using PaladinRogue.Libray.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Gateway.Application.Applications
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