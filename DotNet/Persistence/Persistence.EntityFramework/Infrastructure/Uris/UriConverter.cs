using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.Uris
{
    public class UriConverter : ValueConverter<Uri, string>
    {
        protected UriConverter(ConverterMappingHints mappingHints = null)
            : base(UriConvertTo, UriConvertFrom, mappingHints)
        {
        }

        public static UriConverter Create(ConverterMappingHints mappingHints = null)
        {
            return new UriConverter(mappingHints);
        }

        private static readonly Expression<Func<Uri, string>> UriConvertTo = x => x.ToString();
        private static readonly Expression<Func<string, Uri>> UriConvertFrom = x => TryCreateUri(x);

        private static Uri TryCreateUri(string uriString)
        {
            bool tryCreate = Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out Uri uri);

            return tryCreate ? uri : null;
        }
    }
}