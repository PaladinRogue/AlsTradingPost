using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using PaladinRogue.Library.Core.Domain.DataProtectors;

namespace PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.SecurityKeys
{
    public class SymmetricSecurityKeyConverter: ValueConverter<SymmetricSecurityKey, string>
    {
        private SymmetricSecurityKeyConverter(string keyName, ConverterMappingHints mappingHints = null)
            : base(ConvertTo, ConvertFrom, mappingHints)
        {
            KeyName = keyName;
        }

        private static string KeyName { get; set; }

        public static SymmetricSecurityKeyConverter Create(string keyName, ConverterMappingHints mappingHints = null)
        {
            return new SymmetricSecurityKeyConverter(keyName, mappingHints);
        }

        private static readonly Expression<Func<SymmetricSecurityKey, string>> ConvertTo = x => DataProtection.ProtectAsync(Convert.ToBase64String(x.Key), KeyName).Result;
        private static readonly Expression<Func<string, SymmetricSecurityKey>> ConvertFrom = x => new SymmetricSecurityKey(Convert.FromBase64String(DataProtection.UnprotectAsync<string>(x, KeyName).Result));
    }
}