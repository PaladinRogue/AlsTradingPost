using System;

namespace PaladinRogue.Library.Core.Api.Meta
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