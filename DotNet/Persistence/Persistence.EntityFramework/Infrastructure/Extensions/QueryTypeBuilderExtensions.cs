using System.Reflection;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.Infrastructure.DataProtectors;

namespace Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class QueryTypeBuilderExtensions
    {
        public static QueryTypeBuilder<T> ProtectSensitiveInformation<T>(this QueryTypeBuilder<T> queryTypeBuilder) where T : class
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.DeclaringType == typeof(T) && propertyInfo.GetCustomAttributes<SensitiveInformationAttribute>().Any())
                {
                    IMutableProperty mutableProperty = queryTypeBuilder.Property(propertyInfo.Name).Metadata;
                    mutableProperty.SetValueConverter(SensitiveInformationConverter.Create());
                }
            }

            return queryTypeBuilder;
        }
    }
}