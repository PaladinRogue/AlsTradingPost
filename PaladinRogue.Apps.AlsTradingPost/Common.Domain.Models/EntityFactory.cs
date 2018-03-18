using System;

namespace Common.Domain.Models
{
    public static class EntityFactory
    {
        public static T CreateEntity<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
    }
}
