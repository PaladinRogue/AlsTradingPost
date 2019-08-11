using System;

namespace PaladinRogue.Libray.Core.Api.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LengthAttribute : Attribute
    {
        public int MinLength { get; }

        public int MaxLength { get; }

        public LengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }
    }
}