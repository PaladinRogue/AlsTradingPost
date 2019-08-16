using System;
using System.Collections.Generic;

namespace PaladinRogue.Library.Core.Api.NamingMap
{
    public class NamingMapProvider : INamingMapProvider
    {
        private readonly IDictionary<Type, string> _namingMaps = new Dictionary<Type, string>();

        public NamingMapProvider(IEnumerable<INamingMap> namingMaps)
        {
            foreach (INamingMap namingMap in namingMaps)
            {
                namingMap.Register(this);
            }
        }

        public void AddNamingMap<T>(string mappedName)
        {
            _namingMaps.Add(typeof(T), mappedName);
        }

        public string GetForType(Type type)
        {
            return _namingMaps.ContainsKey(type) ? _namingMaps[type] : type.Name;
        }
    }
}
