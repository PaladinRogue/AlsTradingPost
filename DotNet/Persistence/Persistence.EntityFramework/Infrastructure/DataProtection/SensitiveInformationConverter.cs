using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.EntityFramework.Infrastructure.DataProtection
{
    public class SensitiveInformationConverter : ValueConverter<string, string>
    {
        protected SensitiveInformationConverter(ConverterMappingHints mappingHints = null) 
            : base(SensitiveInformationProtect, SensitiveInformationUnprotect, mappingHints)
        {
        }

        public static SensitiveInformationConverter Create(ConverterMappingHints mappingHints = null)
        {
            return new SensitiveInformationConverter(mappingHints);
        }

        private static readonly Expression<Func<string, string>> SensitiveInformationProtect = x => Common.Domain.Models.DataProtection.DataProtection.Protect(x);
        private static readonly Expression<Func<string, string>> SensitiveInformationUnprotect = x => Common.Domain.Models.DataProtection.DataProtection.Unprotect<string>(x);
    }
}