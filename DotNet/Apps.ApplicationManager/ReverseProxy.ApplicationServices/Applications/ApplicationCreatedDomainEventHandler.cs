using System.Threading.Tasks;
using ApplicationManager.Domain.Applications.Events;
using Common.Domain.DomainEvents.Interfaces;

namespace ReverseProxy.ApplicationServices.Applications
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