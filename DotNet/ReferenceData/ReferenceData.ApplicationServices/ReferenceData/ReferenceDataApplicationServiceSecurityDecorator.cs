using System.Collections.Generic;
using System.Threading.Tasks;
using Authorisation.Application;
using Authorisation.Application.ApplicationServices;
using Authorisation.Application.Contexts;
using ReferenceData.ApplicationServices.ReferenceData.Models;

namespace ReferenceData.ApplicationServices.ReferenceData
{
    public class ReferenceDataApplicationServiceSecurityDecorator : IReferenceDataApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IReferenceDataApplicationService _referenceDataApplicationService;

        public ReferenceDataApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IReferenceDataApplicationService referenceDataApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _referenceDataApplicationService = referenceDataApplicationService;
        }

        public Task<IEnumerable<ReferenceDataValueAdto>> GetAllAsync(GetAllReferenceDataAdto getAllReferenceDataAdto)
        {
            return _securityApplicationService.SecureAsync(() => _referenceDataApplicationService.GetAllAsync(getAllReferenceDataAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.ReferenceData, AuthorisationAction.Search));
        }

        public Task<ReferenceDataValueAdto> GetAsync(GetReferenceDataAdto getReferenceDataAdto)
        {
            return _securityApplicationService.SecureAsync(() => _referenceDataApplicationService.GetAsync(getReferenceDataAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.ReferenceData, AuthorisationAction.Get));
        }
    }
}