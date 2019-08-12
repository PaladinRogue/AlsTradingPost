using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Domain.DataProtectors;
using PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.DataProtectors;

namespace PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.Extensions
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