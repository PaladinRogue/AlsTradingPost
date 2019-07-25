using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Authorisation;
using Common.Authorisation.ApplicationServices;
using Common.Authorisation.Contexts;

namespace Gateway.ApplicationServices.Applications
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