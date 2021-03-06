using PaladinRogue.Library.Core.Domain.DomainEvents.Interfaces;

namespace PaladinRogue.Gateway.Domain.Applications.Events
{
    public class ApplicationCreatedDomainEvent : IDomainEvent
    {
        protected ApplicationCreatedDomainEvent(Application application)
        {
            Application = application;
        }

        public Application Application { get; }

        public static ApplicationCreatedDomainEvent Create(Application application)
        {
            return new ApplicationCreatedDomainEvent(application);
        }
    }
}