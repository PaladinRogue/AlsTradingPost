using System;

namespace Common.Api.Meta
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