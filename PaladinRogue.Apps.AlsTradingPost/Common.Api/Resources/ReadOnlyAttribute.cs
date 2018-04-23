using System;

namespace Common.Api.Resources
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