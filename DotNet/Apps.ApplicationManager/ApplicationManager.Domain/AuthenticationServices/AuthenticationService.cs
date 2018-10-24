using Common.Domain.Models;

namespace ApplicationManager.Domain.AuthenticationServices
{
    public abstract class AuthenticationService : AggregateRoot
    {
        public abstract string TypeDiscriminator { get; }
    }
}
