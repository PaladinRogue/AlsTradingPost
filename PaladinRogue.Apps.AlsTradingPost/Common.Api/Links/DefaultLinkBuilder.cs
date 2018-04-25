using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Api.Routing;

namespace Common.Api.Links
{
    public class DefaultLinkBuilder : ILinkBuilder
    {
        private readonly IRouteProvider<bool> _routeProvider;

        public DefaultLinkBuilder(IRouteProvider<bool> routeProvider)
        {
            _routeProvider = routeProvider;
        }
        
        public IList<Link> BuildLinks<T>(T data)
        {
            return typeof(T).GetCustomAttributes<LinkAttribute>()
                .Select(linkAttribute => new Link
                {
                    Name = linkAttribute.LinkName,
                    AllowVerbs = linkAttribute.HttpVerbs,
                    Uri = _routeProvider.GetRouteTemplate(linkAttribute.UriName, true, data)
                })
                .ToList();
        }
    }
}