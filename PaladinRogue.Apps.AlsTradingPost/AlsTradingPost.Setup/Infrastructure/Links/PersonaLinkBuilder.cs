using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AlsTradingPost.Resources;
using AlsTradingPost.Setup.Infrastructure.Authorisation;
using Common.Api.Links;
using Common.Api.Routing;
using Microsoft.AspNetCore.Http;

namespace AlsTradingPost.Setup.Infrastructure.Links
{
    public class PersonaLinkBuilder : ILinkBuilder
    {
        private readonly IRouteProvider<PersonaFlags> _routeProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PersonaLinkBuilder(IRouteProvider<PersonaFlags> routeProvider, IHttpContextAccessor httpContextAccessor)
        {
            _routeProvider = routeProvider;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public IList<Link> BuildLinks<T>(T data)
        {
            return data.GetType().GetCustomAttributes<LinkAttribute>()
                .Select(linkAttribute => new Link
                {
                    Name = linkAttribute.LinkName,
                    AllowVerbs = linkAttribute.HttpVerbs,
                    Uri = _routeProvider.GetRouteTemplate(linkAttribute.UriName, _httpContextAccessor.CurrentPersonaFlags(), data)
                })
                .ToList();
        }
    }
}