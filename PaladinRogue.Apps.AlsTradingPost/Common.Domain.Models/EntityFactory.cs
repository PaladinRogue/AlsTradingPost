using System;

namespace Common.Domain.Models
{
    public static class EntityFactory
    {
        public static T CreateEntity<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        
        public static T CreateEntity<T>(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            
            return (T)Activator.CreateInstance(typeof(T), id);
        }
    }
}
