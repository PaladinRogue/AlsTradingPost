using System;

namespace ApplicationManager.Domain.Identities.Queries
{
    public interface IGetIdentityBySessionQuery
    {
        Identity Run(Guid sessionId);
    }
}