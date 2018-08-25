using System.Collections.Generic;
using Authentication.Application.Application.Interfaces;
using Authentication.Application.Application.Models;
using Authentication.Domain.ApplicationServices.Interfaces;
using Authentication.Domain.ApplicationServices.Models;
using AutoMapper;
using Common.Domain.Sorting;
using Common.Resources.Authentication;
using Common.Resources.Sorting;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Application
{
    public class ApplicationApplicationKernalService : IApplicationApplicationKernalService
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDomainService _applicationDomainService;
        private readonly ILogger<ApplicationApplicationKernalService> _logger;

        public ApplicationApplicationKernalService(
            IMapper mapper,
            IApplicationDomainService applicationDomainService,
            ILogger<ApplicationApplicationKernalService> logger)
        {
            _mapper = mapper;
            _applicationDomainService = applicationDomainService;
            _logger = logger;
        }

        public IEnumerable<ApplicationAdto> Get(AuthenticationProtocol authenticationProtocol)
        {
            try
            {
                IEnumerable<ApplicationProjection> result = _applicationDomainService.Get(null, a => a.AuthenticationProtocols.HasFlag(authenticationProtocol));

                return _mapper.Map<IEnumerable<ApplicationProjection>, IEnumerable<ApplicationAdto>>(result);
            }
            catch (PropertyNotSortableException e)
            {
                _logger.LogCritical($"Unable to sort on field { e.PropertyName }");
                throw;
            }
        }
    }
}

