using Authentication.Application.Identity.Interfaces;
using Authentication.Application.Identity.Models;
using Authentication.Domain.IdentityServices.Interfaces;
using Authentication.Domain.IdentityServices.Models;
using AutoMapper;
using Common.Application;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.Exceptions;
using Common.Resources.Exceptions;
using Microsoft.Extensions.Logging;

namespace Authentication.Application.Identity
{
    public class IdentityApplicationService : IIdentityApplicationService
    {
        private readonly ILogger<IdentityApplicationService> _logger;
        private readonly IIdentityCommandService _identityCommandService;
        private readonly IIdentityQueryService _identityQueryService;
        private readonly IMapper _mapper;
        private readonly IConcurrencyQueryService<IIdentityQueryService> _concurrencyQueryService;

        public IdentityApplicationService(ILogger<IdentityApplicationService> logger,
            IMapper mapper,
            IIdentityCommandService identityCommandService,
            IIdentityQueryService identityQueryService,
            IConcurrencyQueryService<IIdentityQueryService> concurrencyQueryService)
        {
            _mapper = mapper;
            _identityCommandService = identityCommandService;
            _identityQueryService = identityQueryService;
            _logger = logger;
            _concurrencyQueryService = concurrencyQueryService;
        }

	    public IdentityAdto Get(GetIdentityAdto identity)
	    {
		    IdentityProjection identityProjection = _identityQueryService.GetByAuthenticationId(identity.AuthenticationId) ??
		                             _identityCommandService.Create(_mapper.Map<GetIdentityAdto, CreateIdentityDdto>(identity));

		    return _mapper.Map<IdentityProjection, IdentityAdto>(identityProjection);
        }

        public IdentityAdto Update(UpdateIdentityAdto identity)
        {
            try
            {
                _concurrencyQueryService.CheckConcurrency(identity.Id, identity.Version);

                UpdateIdentityDdto updatedIdentity = _mapper.Map<UpdateIdentityAdto, UpdateIdentityDdto>(identity);

                return _mapper.Map<IdentityProjection, IdentityAdto>(_identityCommandService.Update(updatedIdentity));
            }
            catch (ConcurrencyDomainException e)
            {
                _logger.LogInformation(e, "Concurrency exception");
                throw new AppException(ExceptionType.Concurrency, e);
            }
        }
    }
}

