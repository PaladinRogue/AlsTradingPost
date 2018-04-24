namespace Common.Api.Routing
{
    public interface IRouteProvider
    {
        string GetRouteTemplate<T>(string routeName, T routeData);
        bool HasRoute(string routeName);
    }
}