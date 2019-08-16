namespace PaladinRogue.Library.Core.Setup.Infrastructure.Routing
{
    public interface IRouteProvider<in T>
    {
        string GetRouteTemplate<TRouteData>(string routeName, T routeRestriction, TRouteData routeData);

        bool HasAccessToRoute(string routeName, T routeRestriction);
    }
}