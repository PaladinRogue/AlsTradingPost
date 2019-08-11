using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.DataProtectors;

namespace PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class QueryTypeBuilderExtensions
    {
        public static QueryTypeBuilder<T> ProtectSensitiveInformation<T>(this QueryTypeBuilder<T> queryTypeBuilder) where T : class
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                SensitiveInformationAttribute sensitiveInformationAttribute = propertyInfo.GetCustomAttributes<SensitiveInformationAttribute>().SingleOrDefault();
                if (propertyInfo.DeclaringType == typeof(T) && sensitiveInformationAttribute != null)
                {
                    IMutableProperty mutableProperty = queryTypeBuilder.Property(propertyInfo.Name).Metadata;
                    mutableProperty.SetValueConverter(SensitiveInformationConverter.Create(sensitiveInformationAttribute.KeyName));
                }
            }

            return queryTypeBuilder;
        }
    }
}