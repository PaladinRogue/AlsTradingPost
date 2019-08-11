using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.DataProtectors;
using PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.DataProtectors;

namespace PaladinRogue.Libray.Persistence.EntityFramework.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ProtectSensitiveInformation(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (IMutableProperty mutableProperty in mutableEntityType.GetProperties())
                {
                    SensitiveInformationAttribute sensitiveInformationAttribute = mutableProperty.PropertyInfo?.GetCustomAttributes<SensitiveInformationAttribute>().SingleOrDefault();

                    if (sensitiveInformationAttribute != null)
                    {
                        mutableProperty.SetMaxLength(FieldSizes.Protected);
                        mutableProperty.SetValueConverter(SensitiveInformationConverter.Create(sensitiveInformationAttribute.KeyName));
                    }
                }
            }

            return modelBuilder;
        }
    }
}