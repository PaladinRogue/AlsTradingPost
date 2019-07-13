using System.Threading.Tasks;
using ApplicationManager.Domain.Applications.Events;
using Common.Domain.DomainEvents.Interfaces;

namespace ReverseProxy.ApplicationServices.Applications
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