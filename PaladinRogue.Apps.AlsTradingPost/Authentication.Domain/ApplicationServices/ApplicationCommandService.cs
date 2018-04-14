using System;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.ApplicationServices.Models;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using AutoMapper;
using Common.Domain.Exceptions;
using Common.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Authentication.Domain.ApplicationServices
{
    public class ApplicationCommandService : IApplicationCommandService
	{
        private readonly ILogger<ApplicationCommandService> _logger;
        private readonly IMapper _mapper;
        private readonly IApplicationRepository _identityRepository;

        public ApplicationCommandService(IMapper mapper, IApplicationRepository identityRepository, ILogger<ApplicationCommandService> logger)
        {
            _identityRepository = identityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public ApplicationProjection Create(CreateApplicationDdto entity)
        {
            try
            {
                Application newApplication = _mapper.Map(entity, EntityFactory.CreateEntity<Application>());

                _identityRepository.Add(newApplication);

                return _mapper.Map<Application, ApplicationProjection>(_identityRepository.GetById(newApplication.Id));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create application");
                throw new DomainException("Unable to create application");
            }
        }

        public ApplicationProjection Update(UpdateApplicationDdto entity)
        {
            try
            {
                _identityRepository.Update(_mapper.Map<UpdateApplicationDdto, Application>(entity));

                return _mapper.Map<Application, ApplicationProjection>(_identityRepository.GetById(entity.Id));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update application");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update application");
                throw new DomainException("Unable to update application");
            }
        }
    }
}
