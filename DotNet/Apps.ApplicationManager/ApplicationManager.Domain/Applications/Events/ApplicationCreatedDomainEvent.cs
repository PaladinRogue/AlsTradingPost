using Common.Domain.DomainEvents.Interfaces;
using Common.Domain.Models.Interfaces;

namespace ApplicationManager.Domain.Applications.Events
{
    public class ApplicationCreatedDomainEvent : IAuditedEvent
    {
        protected ApplicationCreatedDomainEvent(Application application)
        {
            Entity = application;
        }

        public IEntity Entity { get; set; }

        public static ApplicationCreatedDomainEvent Create(Application application)
        {
            return new ApplicationCreatedDomainEvent(application);
        }
    }
}
