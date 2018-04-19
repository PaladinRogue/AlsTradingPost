using System;

namespace Common.Api.Builders.Resource.Attributes
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