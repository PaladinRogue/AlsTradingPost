using Common.Domain.DomainEvents.Interfaces;

namespace ApplicationManager.Domain.Users.Events
{
    public class UserCreatedDomainEvent : IDomainEvent
    {
        protected UserCreatedDomainEvent(User user)
        {
            User = user;
        }

        public User User { get; }

        public static UserCreatedDomainEvent Create(User user)
        {
            return new UserCreatedDomainEvent(user);
        }
    }
}
