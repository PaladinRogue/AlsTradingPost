using System;

namespace Common.Api.Resources
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceTypeAttribute : Attribute
    {
        public string Type { get; }

        public ResourceTypeAttribute(string type)
        {
            Type = type;
        }
    }
}