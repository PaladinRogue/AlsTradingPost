﻿using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaladinRogue.Library.Core.Domain.DataProtectors;

namespace PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.DataProtectors
{
    public class SensitiveInformationConverter : ValueConverter<string, string>
    {
        private SensitiveInformationConverter(string keyName, ConverterMappingHints mappingHints = null)
            : base(SensitiveInformationProtect, SensitiveInformationUnprotect, mappingHints)
        {
            KeyName = keyName;
        }

        private static string KeyName { get; set; }

        public static SensitiveInformationConverter Create(string keyName, ConverterMappingHints mappingHints = null)
        {
            return new SensitiveInformationConverter(keyName, mappingHints);
        }

        private static readonly Expression<Func<string, string>> SensitiveInformationProtect = x => DataProtection.ProtectAsync(x, KeyName).Result;
        private static readonly Expression<Func<string, string>> SensitiveInformationUnprotect = x => DataProtection.UnprotectAsync<string>(x, KeyName).Result;
    }
}