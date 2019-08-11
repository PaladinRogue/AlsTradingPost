using System;

namespace PaladinRogue.Libray.Core.Api.NamingMap
{
    public interface INamingMapProvider
    {
        void AddNamingMap<T>(string mappedName);

        string GetForType(Type type);
    }
}
