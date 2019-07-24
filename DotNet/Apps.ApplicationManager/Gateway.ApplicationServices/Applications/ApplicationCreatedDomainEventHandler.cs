using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using Gateway.Domain.Applications.Events;

namespace Gateway.ApplicationServices.Applications
{
    public class ApplicationCreatedDomainEventHandler : IDomainEventHandler<ApplicationCreatedDomainEvent>
    {
        private readonly IApplicationKernalService _applicationKernalService;

        public ApplicationCreatedDomainEventHandler(IApplicationKernalService applicationKernalService)
        {
            _applicationKernalService = applicationKernalService;
        }

        public Task HandleAsync(ApplicationCreatedDomainEvent domainEvent)
        {
            return _applicationKernalService.CreateAsync(domainEvent.Application);
        }
    }
}