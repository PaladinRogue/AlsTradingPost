using Common.Domain.DomainEvents.Interfaces;

namespace ReverseProxy.Domain.Applications.Events
{
    public class ApplicationChangedDomainEvent : IDomainEvent
    {
        protected ApplicationChangedDomainEvent(Application application)
        {
            Application = application;
        }

        public Application Application { get; }

        public static ApplicationChangedDomainEvent Create(Application application)
        {
            return new ApplicationChangedDomainEvent(application);
        }
    }
}