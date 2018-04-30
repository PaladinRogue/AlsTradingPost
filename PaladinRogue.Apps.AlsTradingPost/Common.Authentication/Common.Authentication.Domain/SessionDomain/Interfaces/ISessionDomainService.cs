using System;
using Common.Authentication.Domain.SessionDomain.Models;

namespace Common.Authentication.Domain.SessionDomain.Interfaces
{
    public interface ISessionDomainService
    {
        SessionProjection Refresh(Guid sessionId);
        SessionProjection Create();
    }
}