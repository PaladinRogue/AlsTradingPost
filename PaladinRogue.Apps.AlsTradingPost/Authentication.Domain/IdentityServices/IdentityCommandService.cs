using System;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
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

        public IdentityProjection Create(CreateIdentityDdto entity)
        {
            try
            {
                Identity newIdentity = Mapper.Map(entity, EntityFactory.CreateEntity<Identity>());

                _identityRepository.Add(newIdentity);

                return Mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(newIdentity.Id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw new DomainException("Unable to create admin");
            }
        }
    }
}
