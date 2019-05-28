using System.Collections.Generic;
using System.Reflection;
using Common.Domain.Models.DataProtection;
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
                        mutableProperty.SetMaxLength(1024);
                        mutableProperty.SetValueConverter(SensitiveInformationConverter.Create());
                    }
                }
            }

            return modelBuilder;
        }
    }
}