using System;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionDomainService : ISessionDomainService
    {
        private readonly ISessionCommandService _sessionCommandService;

        public SessionDomainService(ISessionCommandService sessionCommandService)
        {
            _sessionCommandService = sessionCommandService;
        }

        public SessionProjection Refresh(Guid sessionId)
        {
            return new SessionProjection();
        }

        public SessionProjection Create()
        {
            return new SessionProjection();
        }
    }
}