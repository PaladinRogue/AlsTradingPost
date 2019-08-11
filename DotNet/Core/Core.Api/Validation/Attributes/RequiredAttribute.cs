using System;

namespace PaladinRogue.Libray.Core.Api.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute
    {
        public bool IsRequired { get; }

        public RequiredAttribute()
        {
            IsRequired = true;
        }
    }
}