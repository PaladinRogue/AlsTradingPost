using System;

namespace Common.Api.ResourceFormatter.Attributes.Meta
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public bool Required { get; set; }
        
        public RequiredAttribute()
        {
            Required = true;
        }
    }
}