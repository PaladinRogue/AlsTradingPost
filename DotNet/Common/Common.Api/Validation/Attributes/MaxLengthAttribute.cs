using System;

namespace Common.Api.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        public int MaxLength { get; }
        
        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}