using System;

namespace Common.Api.ResourceFormatter.Attributes.Resource
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceNameAttribute : Attribute
    {
        public string ResourceName { get; }

        public ResourceNameAttribute(string resourceName)
        {
            ResourceName = resourceName;
        }
    }
}