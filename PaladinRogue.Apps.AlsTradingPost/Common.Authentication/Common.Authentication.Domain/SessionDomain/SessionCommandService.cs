using System;
using AutoMapper;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.Persistence;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Authentication.Domain.SessionDomain.Models;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionCommandService : ISessionCommandService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ILogger<SessionCommandService> _logger;
        private readonly IMapper _mapper;

        public SessionCommandService(
            ISessionRepository sessionRepository,
            ILogger<SessionCommandService> logger,
            IMapper mapper)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public SessionProjection Create(CreateSessionDdto entity)
        {
            try
            {
                Session newSession = _mapper.Map(entity, EntityFactory.CreateEntity<Session>(entity.Id));

                _sessionRepository.Add(newSession);

                return _mapper.Map<Session, SessionProjection>(_sessionRepository.GetById(newSession.Id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw new DomainException("Unable to create admin");
            }
        }

        public SessionProjection Update(UpdateSessionDdto entity)
        {
            try
            {
                _sessionRepository.Update(_mapper.Map<UpdateSessionDdto, Session>(entity));

                return _mapper.Map<Session, SessionProjection>(_sessionRepository.GetById(entity.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update session");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update session");
                throw new DomainException("Unable to update session");
            }
        }
    }
}