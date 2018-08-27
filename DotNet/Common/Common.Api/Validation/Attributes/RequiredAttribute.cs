using System;

namespace Common.Api.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public bool IsRequired { get; set; }
        
        public RequiredAttribute()
        {
            IsRequired = true;
        }
    }
}