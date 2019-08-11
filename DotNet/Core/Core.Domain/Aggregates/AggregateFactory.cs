using System;

namespace PaladinRogue.Libray.Core.Domain.Aggregates
{
    public static class AggregateFactory
    {
        public static T CreateRoot<T>() where T : IAggregateRoot
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public static T CreateRoot<T>(Guid id) where T : IAggregateRoot
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return (T)Activator.CreateInstance(typeof(T), id);
        }
    }
}
