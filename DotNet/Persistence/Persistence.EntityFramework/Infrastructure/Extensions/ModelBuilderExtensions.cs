using System.Collections.Generic;
using System.Reflection;
using Common.Domain.DataProtection;
using Common.Domain.Persistence;
using Common.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Persistence.EntityFramework.Infrastructure.DataProtection;

namespace Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ProtectSensitiveInformation(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty mutableProperty in mutableEntityType.GetProperties())
                {
                    IEnumerable<SensitiveInformationAttribute> attributes = mutableProperty.PropertyInfo?.GetCustomAttributes<SensitiveInformationAttribute>();

                    if (attributes != null && attributes.Any())
                    {
                        mutableProperty.SetMaxLength(FieldSizes.Protected);
                        mutableProperty.SetValueConverter(SensitiveInformationConverter.Create());
                    }
                }
            }

            return modelBuilder;
        }
    }
}