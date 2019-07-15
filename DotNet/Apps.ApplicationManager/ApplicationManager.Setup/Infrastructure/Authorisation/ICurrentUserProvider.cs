using System;

namespace ApplicationManager.Setup.Infrastructure.Authorisation
{
    public interface ICurrentUserProvider
    {
        bool IsAuthenticated { get; }

        Guid? Id { get; }
    }
}
