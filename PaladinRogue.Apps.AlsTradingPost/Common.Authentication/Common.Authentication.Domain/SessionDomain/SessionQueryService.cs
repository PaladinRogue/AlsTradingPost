using System;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.Persistence;
using Common.Authentication.Domain.SessionDomain.Interfaces;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionQueryService : ISessionQueryService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionQueryService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public Session GetById(Guid id)
        {
            return _sessionRepository.GetById(id);
        }
    }
}