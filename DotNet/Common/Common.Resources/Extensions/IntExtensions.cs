using System;

namespace Common.Resources.Extensions
{
    public static class IntExtensions
    {
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
            => (TEnum) (object) value;
    }
}