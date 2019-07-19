using System;
using System.Linq.Expressions;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.EntityFramework.Infrastructure.DataProtectors
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

        private static readonly Expression<Func<string, string>> SensitiveInformationProtect = x => DataProtection.Protect(x);
        private static readonly Expression<Func<string, string>> SensitiveInformationUnprotect = x => DataProtection.Unprotect<string>(x);
    }
}