using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;

namespace PaladinRogue.Library.Persistence.EntityFramework.Infrastructure.DateTimeConverters
{
    public class InstantConverter : ValueConverter<Instant, DateTime>
    {
        protected InstantConverter(ConverterMappingHints mappingHints = null)
            : base(InstantConvertTo, InstantConvertFrom, mappingHints)
        {
        }

        public static InstantConverter Create(ConverterMappingHints mappingHints = null)
        {
            return new InstantConverter(mappingHints);
        }

        private static readonly Expression<Func<Instant, DateTime>> InstantConvertTo = x => x.ToDateTimeUtc();
        private static readonly Expression<Func<DateTime, Instant>> InstantConvertFrom = x => LocalDateTime.FromDateTime(x).InUtc().ToInstant();
    }
}