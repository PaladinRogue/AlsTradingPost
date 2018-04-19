using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.Builders.Attributes;
using Common.Api.Builders.Resource.Attributes;
using Common.Api.Builders.Template.Attributes;
using Common.Api.Sorting;
using Common.Api.Validation;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;

namespace Common.Api.Builders
{
    public static class BuilderHelper
    {
        public static Data<T> FormatTemplateData<T>(T templateData)
        {
            NameAttribute nameAttribute =
                templateData.GetType().GetCustomAttribute<NameAttribute>();

            return new Data<T>
            {
                TemplateTypeName = nameAttribute?.Name ?? FormatTemplateName(templateData.GetType().Name),
                Resource = templateData
            };
        }

        public static Meta FormatTemplateMeta<T>(T templateData)
        {
            IList<PropertyMeta> properties = new List<PropertyMeta>();
            IList<string> sortableFields = new List<string>();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(templateData.GetType()))
            {
                IList<Constraint> constraints = new List<Constraint>
                {
                    new Constraint
                    {
                        Name = FieldMeta.Type,
                        Value = property.PropertyType.Name
                    }
                };

                foreach (RequiredAttribute requiredAttribute in property.Attributes.OfType<RequiredAttribute>())
                {
                    constraints.Add(new Constraint
                    {
                        Name = ValidationMeta.Required,
                        Value = requiredAttribute.IsRequired
                    });
                }

                foreach (MinLengthAttribute minLengthAttribute in property.Attributes.OfType<MinLengthAttribute>())
                {
                    constraints.Add(new Constraint
                    {
                        Name = ValidationMeta.MinLength,
                        Value = minLengthAttribute.MinLength
                    });
                }

                foreach (MaxLengthAttribute maxLengthAttribute in property.Attributes.OfType<MaxLengthAttribute>())
                {
                    constraints.Add(new Constraint
                    {
                        Name = ValidationMeta.MaxLength,
                        Value = maxLengthAttribute.MaxLength
                    });
                }

                foreach (LengthAttribute lengthAttribute in property.Attributes.OfType<LengthAttribute>())
                {
                    constraints.Add(new Constraint
                    {
                        Name = ValidationMeta.MinLength,
                        Value = lengthAttribute.MinLength
                    });
                    constraints.Add(new Constraint
                    {
                        Name = ValidationMeta.MaxLength,
                        Value = lengthAttribute.MaxLength
                    });
                }

                foreach (HiddenAttribute hiddenAttribute in property.Attributes.OfType<HiddenAttribute>())
                {
                    constraints.Add(new Constraint
                    {
                        Name = FieldMeta.Hidden,
                        Value = hiddenAttribute.IsHidden
                    });
                }

                if (property.Attributes.OfType<SortableAttribute>().Any())
                {
                    sortableFields.Add(property.Name.ToCamelCase());
                }

                if (constraints.Any())
                {
                    properties.Add(new PropertyMeta
                    {
                        Name = property.Name,
                        Constraints = constraints
                    });
                }
            }

            if (templateData is IThenByTemplate thenByTemplate)
            {
                properties.Single(p => p.Name == nameof(thenByTemplate.ThenBy)).Constraints.Add(new Constraint
                {
                    Name = FieldMeta.Values,
                    Value = sortableFields
                });
            }
            
            if (templateData is IOrderByTemplate orderByTemplate)
            {
                properties.Single(p => p.Name == nameof(orderByTemplate.OrderBy)).Constraints.Add(new Constraint
                {
                    Name = FieldMeta.Values,
                    Value = sortableFields
                });
            }

            NameAttribute nameAttribute =
                templateData.GetType().GetCustomAttribute<NameAttribute>();

            return new Meta
            {
                TemplateTypeName = nameAttribute?.Name ?? FormatTemplateName(templateData.GetType().Name),
                Properties = properties
            };
        }
        
        private static string FormatTemplateName(string resourceName)
        {
            return resourceName.Replace("Template", string.Empty);
        }
        
        private static string FormatResourceName(string resourceName)
        {
            return resourceName.Replace("Resource", string.Empty);
        }
    }
}