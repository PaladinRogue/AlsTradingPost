using System;

namespace Common.Api.Resources
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HiddenAttribute : Attribute
    {
        public bool IsHidden { get; }

        public HiddenAttribute()
        {
            IsHidden = true;
        }
    }
}