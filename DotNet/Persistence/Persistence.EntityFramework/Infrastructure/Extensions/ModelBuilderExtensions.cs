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
                    object[] attributes = mutableProperty.PropertyInfo?.GetCustomAttributes(typeof(SensitiveInformationAttribute), false);
                    
                    if (attributes != null && attributes.Any())
                    {
                        if (mutableProperty.GetMaxLength() < 64)
                        {
                            mutableProperty.SetMaxLength(64);
                        }
                        mutableProperty.SetValueConverter(SensitiveInformationConverter.Create());
                    }
                }
            }

            return modelBuilder;
        }
    }
}