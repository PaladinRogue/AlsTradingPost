using System;

namespace Common.Domain.Sorting
{
    [Serializable]
    public class PropertyNotSortableException : Exception
    {
        public string PropertyName { get; set; }
        
        public PropertyNotSortableException(string propertyName) : this(propertyName, string.Empty)
        {
        }

        public PropertyNotSortableException(string propertyName, string message) : this(propertyName, message, null)
        {
        }

        public PropertyNotSortableException(string propertyName, string message, Exception innerException) : base(message, innerException)
        {
            PropertyName = propertyName;
        }
    }
}
