using System.Collections.Generic;
using System.Threading.Tasks;
using PaladinRogue.Libray.Authorisation.Application.ApplicationServices;
using PaladinRogue.Libray.Authorisation.Common;
using PaladinRogue.Libray.Authorisation.Common.Contexts;

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