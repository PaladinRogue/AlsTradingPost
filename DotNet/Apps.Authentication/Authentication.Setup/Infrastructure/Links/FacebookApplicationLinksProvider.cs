using System.Collections.Generic;
using System.Linq;
using Authentication.Application.Application.Interfaces;
using Authentication.Application.Application.Models;
using Common.Api.Links;
using Common.Resources.Authentication;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Setup.Infrastructure.Links
{
    public class FacebookApplicationLinksProvider : IDynamicLinksProvider
    {
        private readonly IApplicationApplicationKernalService _applicationApplicationKernalService;

        public FacebookApplicationLinksProvider(
            IApplicationApplicationKernalService applicationApplicationKernalService)
        {
            _applicationApplicationKernalService = applicationApplicationKernalService;
        }

        public IEnumerable<ILink> GetLinks()
        {
            IEnumerable<ApplicationAdto> applications = _applicationApplicationKernalService.Get(AuthenticationProtocol.Facebook);

            return applications.Select(applicationAdto => new Link
                {
                    Name = applicationAdto.Name,
                    AllowVerbs = HttpVerb.Get,
                    Uri = applicationAdto.AuthenticationEndpoint
                });
        }
    }
}
