using System;

namespace Common.Api.Resources
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortableAttribute : Attribute
    {
        public bool IsSortable { get; set; }

        public SortableAttribute()
        {
            IsSortable = true;
        }
    }
}