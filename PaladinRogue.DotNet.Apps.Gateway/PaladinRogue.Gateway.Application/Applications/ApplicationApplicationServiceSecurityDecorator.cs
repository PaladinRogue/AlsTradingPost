using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Library.Authorisation.Common;
using PaladinRogue.Library.Authorisation.Common.Authorisation;
using PaladinRogue.Library.Authorisation.Common.Contexts;

namespace PaladinRogue.Gateway.Application.Applications
{
    public class ApplicationApplicationServiceSecurityDecorator : IApplicationApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;

        private readonly IApplicationApplicationService _applicationApplicationService;

        public ApplicationApplicationServiceSecurityDecorator(
            ISecurityApplicationService securityApplicationService,
            IApplicationApplicationService applicationApplicationService)
        {
            _securityApplicationService = securityApplicationService;
            _applicationApplicationService = applicationApplicationService;
        }

        public Task<IEnumerable<ApplicationAdto>> GetAllAsync()
        {
            return _securityApplicationService.SecureAsync(() => _applicationApplicationService.GetAllAsync(),
                DefaultAuthorisationContext.Create(AuthorisationResource.Application, AuthorisationAction.Search));
        }
    }
}