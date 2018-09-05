namespace Common.Api.NamingMap
{
    public interface INamingMap
    {
        void Register(INamingMapProvider namingMapProvider);
    }
}
