using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Domain.Models.Interfaces;

namespace Persistence.EntityFramework.Infrastructure.Extensions
{
    public class EntityComparer<T> : IEqualityComparer<T> where T : class, IEntity
    {
        private readonly IEnumerable<PropertyInfo> _properties;
        public EntityComparer()
        {
            _properties = typeof(T).GetProperties().Where(e => e.Name != "Version");
        }

        public bool Equals(T x, T y)
        {
            return GetHashCode(x) == GetHashCode(y);
        }

        public int GetHashCode(T obj)
        {
            return _properties.Where(propertyInfo => propertyInfo.GetValue(obj) != null).Aggregate(15, (current, propertyInfo) => current * 5 + propertyInfo.GetValue(obj).GetHashCode());
        }
    }
}