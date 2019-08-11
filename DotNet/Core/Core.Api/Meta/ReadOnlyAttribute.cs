using System;

namespace PaladinRogue.Libray.Core.Api.Meta
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReadOnlyAttribute : Attribute
    {
        public bool IsReadOnly { get; }

        public ReadOnlyAttribute()
        {
            IsReadOnly = true;
        }
    }
}