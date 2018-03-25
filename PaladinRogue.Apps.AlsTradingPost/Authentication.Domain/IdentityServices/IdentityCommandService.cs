using System;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using Authentication.Domain.Models;
using Authentication.Persistence.Interfaces;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Authentication.Domain.IdentityServices
{
    public class IdentityCommandService : IIdentityCommandService
    {
        private readonly ILogger<IdentityCommandService> _logger;
        private readonly IMapper _mapper;
        private readonly IIdentityRepository _identityRepository;

        public IdentityCommandService(IMapper mapper, IIdentityRepository identityRepository, ILogger<IdentityCommandService> logger)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public IdentityProjection Create(CreateIdentityDdto entity)
        {
            try
            {
                var newIdentity = _mapper.Map(entity, EntityFactory.CreateEntity<Identity>());

                _identityRepository.Add(newIdentity);

                return _mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(newIdentity.Id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw new DomainException("Unable to create admin");
            }
        }

        public IdentityProjection Update(UpdateIdentityDdto entity)
        {
            try
            {
                _identityRepository.Update(_mapper.Map<UpdateIdentityDdto, Identity>(entity));

                return _mapper.Map<Identity, IdentityProjection>(_identityRepository.GetById(entity.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to create admin");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update admin");
                throw new DomainException("Unable to update admin");
            }
        }
    }
}
