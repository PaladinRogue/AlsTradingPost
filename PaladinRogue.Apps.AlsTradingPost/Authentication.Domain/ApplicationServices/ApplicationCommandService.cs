using System;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.Models;
using Authentication.Domain.Persistence;
using Common.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Authentication.Domain.ApplicationServices
{
    public class ApplicationCommandService : IApplicationCommandService
	{
        private readonly ILogger<ApplicationCommandService> _logger;
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationCommandService(IApplicationRepository identityRepository, ILogger<ApplicationCommandService> logger)
        {
            _applicationRepository = identityRepository;
            _logger = logger;
        }

        public Guid Create(Application entity)
        {
            try
            {
                _applicationRepository.Add(entity);

                return entity.Id;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to create application");
                throw new CreateDomainException(entity, e);
            }
        }

        public void Update(Application entity)
        {
            try
            {
                _applicationRepository.Update(entity);
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogCritical(e, "Unable to update application");
                throw;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to update application");
                throw new UpdateDomainException(entity, e);
            }
        }
    }
}
