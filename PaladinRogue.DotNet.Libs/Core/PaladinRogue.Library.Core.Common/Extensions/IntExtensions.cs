using System;

namespace PaladinRogue.Library.Core.Common.Extensions
{
    public static class IntExtensions
    {
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
            => (TEnum) (object) value;
    }
}