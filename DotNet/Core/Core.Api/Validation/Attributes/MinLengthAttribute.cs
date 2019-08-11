using System;

namespace PaladinRogue.Libray.Core.Api.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinLengthAttribute : Attribute
    {
        public int MinLength { get; }

        public MinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }
    }
}