﻿using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.DataProtectors;

namespace PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {
        public static EntityTypeBuilder<T> ProtectSensitiveInformation<T>(this EntityTypeBuilder<T> entityTypeBuilder) where T : class
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                SensitiveInformationAttribute sensitiveInformationAttribute = propertyInfo.GetCustomAttributes<SensitiveInformationAttribute>().SingleOrDefault();

                if (propertyInfo.DeclaringType == typeof(T) && sensitiveInformationAttribute != null)
                {
                    IMutableProperty mutableProperty = entityTypeBuilder.Property(propertyInfo.Name).Metadata;

                    mutableProperty.SetMaxLength(FieldSizes.Protected);
                    mutableProperty.SetValueConverter(SensitiveInformationConverter.Create(sensitiveInformationAttribute.KeyName));
                }
            }

            return entityTypeBuilder;
        }
    }
}