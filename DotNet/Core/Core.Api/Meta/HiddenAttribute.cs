using System;

namespace PaladinRogue.Library.Core.Api.Meta
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