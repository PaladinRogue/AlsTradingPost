using System;

namespace Common.Api.Builders.Template.Attributes
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