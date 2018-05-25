using System;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Authentication.Domain.IdentityServices
{
    public class IdentityCommandService : IIdentityCommandService
    {
        private readonly ILogger<IdentityCommandService> _logger;
        private readonly IIdentityRepository _identityRepository;

        public IdentityCommandService(IIdentityRepository identityRepository, ILogger<IdentityCommandService> logger)
        {
            _identityRepository = identityRepository;
            _logger = logger;
        }

        public Guid Create(Identity entity)
        {
            try
            {
                _identityRepository.Add(entity);

                return entity.Id;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw new CreateDomainException(entity, e);
            }
        }
    }
}
