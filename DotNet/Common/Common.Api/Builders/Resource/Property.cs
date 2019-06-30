using System;

namespace Common.Api.Builders.Resource
{
    public class Property
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public string FieldType { get; set; }

        public Type Type { get; set; }

        public Constraints Constraints { get; set; }

        public ValidationConstraints ValidationConstraints { get; set; }
    }
}
