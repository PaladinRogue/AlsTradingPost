using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Application.ApplicationServices;
using PaladinRogue.Libray.Authorisation.Common;
using PaladinRogue.Libray.Authorisation.Common.Contexts;
using PaladinRogue.Libray.ReferenceData.Application.ReferenceData.Models;

namespace PaladinRogue.Libray.ReferenceData.Application.ReferenceData
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