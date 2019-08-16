using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common;
using PaladinRogue.Library.Authorisation.Common.Authorisation;
using PaladinRogue.Library.Authorisation.Common.Contexts;
using PaladinRogue.Library.ReferenceData.Application.ReferenceData.Models;

namespace PaladinRogue.Library.ReferenceData.Application.ReferenceData
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