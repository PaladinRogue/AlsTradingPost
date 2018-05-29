using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.Authorisation;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Routing;
using Common.Application.Authorisation;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Links
{
    public class PersonaLinkBuilder : ILinkBuilder
    {
        private readonly IRouteProvider<PersonaFlags> _routeProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorisationManager _authorisationManager;

        public PersonaLinkBuilder(
            IRouteProvider<PersonaFlags> routeProvider,
            IHttpContextAccessor httpContextAccessor,
            IAuthorisationManager authorisationManager)
        {
            _routeProvider = routeProvider;
            _httpContextAccessor = httpContextAccessor;
            _authorisationManager = authorisationManager;
        }

        public IList<Link> BuildLinks<T>(T data)
        {
            return data.GetType().GetCustomAttributes<LinkAttribute>()
                .GroupBy(linkAttribute => new
                {
                    linkAttribute.LinkName,
                    linkAttribute.UriName
                })
                .Select(linkAttributeGrouping => new Link
                {
                    Name = linkAttributeGrouping.Key.LinkName,
                    AllowVerbs = linkAttributeGrouping
                        .Where(linkAttribute =>
                        {
                            if (linkAttribute.AuthorisationContextType == null)
                            {
                                return true;
                            }

                            if (data.GetType().IsInstanceOfType(typeof(IEntityResource)))
                            {
                                throw new ArgumentException("A link specified with an authorisation context must be an instance of IEntityResource");
                            }

                            IAuthorisationContext authorisationContext = (IAuthorisationContext)Activator.CreateInstance(linkAttribute.AuthorisationContextType, data);
                            return _authorisationManager.HasAccess(authorisationContext);
                        })
                        .Select(linkAttribute => linkAttribute.HttpVerb).ToArray(),
                    Uri = _routeProvider.GetRouteTemplate(linkAttributeGrouping.Key.UriName,
                        _httpContextAccessor.CurrentPersonaFlags(), data)
                })
                .Where(link => link.AllowVerbs.Any())
                .ToList();
        }
    }
}