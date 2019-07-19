using System;
using System.Linq.Expressions;
using Common.Domain.DataProtectors;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.EntityFramework.Infrastructure.SecurityKeys
{
    public class SymmetricSecurityKeyConverter: ValueConverter<SymmetricSecurityKey, string>
    {
        protected SymmetricSecurityKeyConverter(ConverterMappingHints mappingHints = null)
            : base(ConvertTo, ConvertFrom, mappingHints)
        {
        }

        public static SymmetricSecurityKeyConverter Create(ConverterMappingHints mappingHints = null)
        {
            return new SymmetricSecurityKeyConverter(mappingHints);
        }

        private static readonly Expression<Func<SymmetricSecurityKey, string>> ConvertTo = x => DataProtection.Protect(Convert.ToBase64String(x.Key));
        private static readonly Expression<Func<string, SymmetricSecurityKey>> ConvertFrom = x => new SymmetricSecurityKey(Convert.FromBase64String(DataProtection.Unprotect<string>(x)));
    }
}