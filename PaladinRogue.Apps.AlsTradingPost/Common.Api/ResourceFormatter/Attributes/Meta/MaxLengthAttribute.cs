using System;

namespace Common.Api.ResourceFormatter.Attributes.Meta
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