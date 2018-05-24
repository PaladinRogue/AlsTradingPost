using System;
using Common.Authentication.Domain.Models;
using Common.Authentication.Domain.Persistence;
using Common.Authentication.Domain.SessionDomain.Interfaces;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Common.Authentication.Domain.SessionDomain
{
    public class SessionCommandService : ISessionCommandService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ILogger<SessionCommandService> _logger;

        public SessionCommandService(
            ISessionRepository sessionRepository,
            ILogger<SessionCommandService> logger)
        {
            _sessionRepository = sessionRepository;
            _logger = logger;
        }

        public Guid Create(Session entity)
        {
            try
            {
                _sessionRepository.Add(entity);

                return entity.Id;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw new CreateDomainException(entity, e);
            }
        }

        public void Update(Session entity)
        {
            try
            {
                _sessionRepository.Update(entity);
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update session");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update session");
                throw new UpdateDomainException(entity, e);
            }
        }
    }
}