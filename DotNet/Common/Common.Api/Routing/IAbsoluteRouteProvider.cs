namespace Common.Api.Routing
{
    public interface IAbsoluteRouteProvider
    {
        string GetRouteTemplate<TRouteData>(string routeName, TRouteData routeData);
    }
}