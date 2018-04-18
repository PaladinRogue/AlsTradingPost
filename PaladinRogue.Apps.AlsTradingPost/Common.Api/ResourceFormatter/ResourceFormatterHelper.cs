using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.Pagination.Interfaces;
using Common.Api.ResourceFormatter.Attributes;
using Common.Api.Sorting;
using Common.Api.Validation;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;
using Microsoft.Data.OData.Query.SemanticAst;

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
            Dictionary<string, IDictionary<string, object>> properties = new Dictionary<string, IDictionary<string, object>>();
            List<string> sortableFields = new List<string>();
            
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(resource.GetType()))
            {
                IDictionary<string, object> constraints = new Dictionary<string, object>();

                foreach (RequiredAttribute requiredAttribute in property.Attributes.OfType<RequiredAttribute>())
                {
                    constraints.Add(ValidationMeta.Required, requiredAttribute.IsRequired);
                }

                foreach (MinLengthAttribute minLengthAttribute in property.Attributes.OfType<MinLengthAttribute>())
                {
                    constraints.Add(ValidationMeta.MinLength, minLengthAttribute.MinLength);
                }

                foreach (MaxLengthAttribute maxLengthAttribute in property.Attributes.OfType<MaxLengthAttribute>())
                {
                    constraints.Add(ValidationMeta.MaxLength, maxLengthAttribute.MaxLength);
                }

                foreach (LengthAttribute lengthAttribute in property.Attributes.OfType<LengthAttribute>())
                {
                    constraints.Add(ValidationMeta.MaxLength, lengthAttribute.MaxLength);
                    constraints.Add(ValidationMeta.MinLength, lengthAttribute.MinLength);
                }

                foreach (HiddenAttribute hiddenAttribute in property.Attributes.OfType<HiddenAttribute>())
                {
                    constraints.Add(FieldMeta.Hidden, hiddenAttribute.IsHidden);
                }

                sortableFields.AddRange(
                    from sortableAttribute in property.Attributes.OfType<SortableAttribute>()
                    where sortableAttribute.IsSortable
                    select property.Name.ToCamelCase()
                );

                if (constraints.Any())
                {
                    properties.Add(property.Name, constraints);
                }
            }

            if (resource is IOrderByTemplate orderByTemplate)
            {
                const string propertyName = nameof(orderByTemplate.OrderBy);

                if (properties.ContainsKey(propertyName))
                {
                    properties[propertyName].Add(FieldMeta.Values, sortableFields);
                }
                else
                {
                    properties.Add(propertyName, new Dictionary<string, object>());
                    properties[propertyName].Add(FieldMeta.Values, sortableFields);
                }
            }

            ResourceNameAttribute resourceNameAttribute = resource.GetType().GetCustomAttribute<ResourceNameAttribute>();

            return new Dictionary<string, object> {
                { resourceNameAttribute?.ResourceName ?? FormatResourceName(resource.GetType().Name), properties }
            };
        }
    }
}