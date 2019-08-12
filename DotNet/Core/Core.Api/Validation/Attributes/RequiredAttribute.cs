using System;

namespace PaladinRogue.Library.Core.Api.Validation.Attributes
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