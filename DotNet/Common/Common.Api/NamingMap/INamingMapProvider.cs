using System;

namespace Common.Api.NamingMap
{
    public interface INamingMapProvider
    {
        void AddNamingMap<T>(string mappedName);

        string GetForType(Type type);
    }
}
