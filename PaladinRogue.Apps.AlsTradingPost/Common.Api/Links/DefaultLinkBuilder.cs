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
            return data.GetType().GetCustomAttributes<LinkAttribute>()
                .GroupBy(linkAttribute => new
                {
                    linkAttribute.LinkName,
                    linkAttribute.UriName
                })
                .Select(linkAttributeGrouping => new Link
                {
                    Name = linkAttributeGrouping.Key.LinkName,
                    AllowVerbs = linkAttributeGrouping.Select(linkAttribute => linkAttribute.HttpVerb).ToArray(),
                    Uri = _routeProvider.GetRouteTemplate(linkAttributeGrouping.Key.UriName, true, data)
                })
                .ToList();
        }
    }
}