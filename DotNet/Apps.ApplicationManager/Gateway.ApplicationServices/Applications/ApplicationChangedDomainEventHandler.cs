using System.Threading.Tasks;
using Common.Domain.DomainEvents.Interfaces;
using Gateway.Domain.Applications.Events;

namespace Gateway.ApplicationServices.Applications
{
    public class ApplicationChangedDomainEventHandler : IDomainEventHandler<ApplicationChangedDomainEvent>
    {
        private readonly IApplicationKernalService _applicationKernalService;

        public ApplicationChangedDomainEventHandler(IApplicationKernalService applicationKernalService)
        {
            _applicationKernalService = applicationKernalService;
        }

        public Task HandleAsync(ApplicationChangedDomainEvent domainEvent)
        {
            return _applicationKernalService.UpdateAsync(domainEvent.Application);
        }
    }
}