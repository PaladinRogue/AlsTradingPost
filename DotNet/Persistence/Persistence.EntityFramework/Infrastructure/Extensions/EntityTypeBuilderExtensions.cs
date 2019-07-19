﻿using System.Reflection;
using Common.Domain.DataProtectors;
using Common.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.EntityFramework.Infrastructure.DataProtectors;

namespace Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> ProtectSensitiveInformation<T>(this EntityTypeBuilder<T> entityTypeBuilder) where T : class
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.DeclaringType == typeof(T) && propertyInfo.GetCustomAttributes<SensitiveInformationAttribute>().Any())
                {
                    IMutableProperty mutableProperty = entityTypeBuilder.Property(propertyInfo.Name).Metadata;

                    mutableProperty.SetMaxLength(FieldSizes.Protected);
                    mutableProperty.SetValueConverter(SensitiveInformationConverter.Create());
                }
            }

            return entityTypeBuilder;
        }
    }
}