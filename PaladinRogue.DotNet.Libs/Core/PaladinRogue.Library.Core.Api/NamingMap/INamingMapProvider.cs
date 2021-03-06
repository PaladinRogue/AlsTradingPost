﻿using System;

namespace PaladinRogue.Library.Core.Api.NamingMap
{
    public interface INamingMapProvider
    {
        void AddNamingMap<T>(string mappedName);

        string GetForType(Type type);
    }
}
