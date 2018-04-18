using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.ResourceFormatter.Attributes.Meta;
using Common.Api.ResourceFormatter.Attributes.Resource;
using Common.Api.Validation;

namespace Common.Api.ResourceFormatter
{
    public static class ResourceFormatterHelper
    {
        private static string FormatAttributeName(Attribute attribute)
        {
            return attribute.GetType().Name;
        }
        
        private static string FormatResourceName(string resourceName)
        {
            return resourceName.Replace("Resource", string.Empty).Replace("Template", string.Empty);
        }
        
        public static IDictionary<string, object> FormatResourceData(object resource)
        {
            ResourceNameAttribute resourceNameAttribute = resource.GetType().GetCustomAttribute<ResourceNameAttribute>();
            
            return new Dictionary<string, object> {
                { resourceNameAttribute?.ResourceName ?? FormatResourceName(resource.GetType().Name), resource }
            };
        }
        
        public static IDictionary<string, object> FormatResourceMeta(object resource)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(resource.GetType()))
            {
                IDictionary<string, object> constraints = new Dictionary<string, object>();

                foreach (RequiredAttribute requiredAttribute in property.Attributes.OfType<RequiredAttribute>())
                {
                    constraints.Add(ValidationTypes.Required, requiredAttribute.Required);
                }

                foreach (MinLengthAttribute minLengthAttribute in property.Attributes.OfType<MinLengthAttribute>())
                {
                    constraints.Add(ValidationTypes.MinLength, minLengthAttribute.MinLength);
                }

                foreach (MaxLengthAttribute maxLengthAttribute in property.Attributes.OfType<MaxLengthAttribute>())
                {
                    constraints.Add(ValidationTypes.MaxLength, maxLengthAttribute.MaxLength);
                }

                foreach (LengthAttribute lengthAttribute in property.Attributes.OfType<LengthAttribute>())
                {
                    constraints.Add(ValidationTypes.MaxLength, lengthAttribute.MaxLength);
                    constraints.Add(ValidationTypes.MinLength, lengthAttribute.MinLength);
                }
                
                if (constraints.Any())
                {
                    properties.Add(property.Name, constraints);
                }
            }

            ResourceNameAttribute resourceNameAttribute = resource.GetType().GetCustomAttribute<ResourceNameAttribute>();

            return new Dictionary<string, object> {
                { resourceNameAttribute?.ResourceName ?? FormatResourceName(resource.GetType().Name), properties }
            };;
        }
    }
}