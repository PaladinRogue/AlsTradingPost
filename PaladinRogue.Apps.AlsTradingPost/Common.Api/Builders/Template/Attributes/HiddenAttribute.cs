using System;

namespace Common.Api.Builders.Template.Attributes
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