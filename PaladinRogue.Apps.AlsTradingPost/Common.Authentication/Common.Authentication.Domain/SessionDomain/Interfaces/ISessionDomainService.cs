using System;
using Common.Authentication.Domain.SessionDomain.Exceptions;
using Common.Authentication.Domain.SessionDomain.Models;

namespace Common.Authentication.Domain.SessionDomain.Interfaces
{
    public interface ISessionDomainService
    {
        /// <exception cref="RefreshTokenInvalidDomainException"></exception>
        /// <exception cref="SessionRevokedDomainException"></exception>
        RefreshSessionProjection Refresh(RefreshSessionDdto refreshSessionDdto);
        SessionProjection Create(Guid sessionId);
    }
}