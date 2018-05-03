using System;
using AutoMapper;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.Persistence;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionQueryService : ISessionQueryService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public SessionQueryService(ISessionRepository sessionRepository, IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public SessionProjection GetById(Guid id)
        {
            return _mapper.Map<Session, SessionProjection>(_sessionRepository.GetById(id));
        }
    }
}