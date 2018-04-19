using System;
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
            return new Data<T>
            {
                TemplateTypeName = GetTypeName(templateData),
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
                        Value = FieldTypeMapper.GetFieldType(property.PropertyType)
                    }
                };

                AddAttributeConstraint(constraints, property,
                    CreateAttributeKeyValuePair<RequiredAttribute, bool>(
                        ValidationMeta.Required,
                        a => a.IsRequired
                    ));

                AddAttributeConstraint(constraints, property,
                    CreateAttributeKeyValuePair<MinLengthAttribute, int>(
                        ValidationMeta.MinLength,
                        a => a.MinLength
                    ));

                AddAttributeConstraint(constraints, property,
                    CreateAttributeKeyValuePair<MaxLengthAttribute, int>(
                        ValidationMeta.MaxLength,
                        a => a.MaxLength
                    ));

                AddAttributeConstraint(constraints, property,
                    CreateAttributeKeyValuePair<LengthAttribute, int>(
                        ValidationMeta.MinLength,
                        a => a.MinLength
                    ), CreateAttributeKeyValuePair<LengthAttribute, int>(
                        ValidationMeta.MaxLength,
                        a => a.MaxLength
                    ));

                AddAttributeConstraint(constraints, property,
                    CreateAttributeKeyValuePair<HiddenAttribute, bool>(
                        FieldMeta.Hidden,
                        a => a.IsHidden
                    ));

                if (property.Attributes.OfType<SortableAttribute>().Any())
                {
                    sortableFields.Add(property.Name.ToCamelCase());
                }
                
                properties.Add(new PropertyMeta
                {
                    Name = property.Name,
                    Constraints = constraints
                });
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


            return new Meta
            {
                TemplateTypeName = GetTypeName(templateData),
                Properties = properties
            };
        }

        private static string GetTypeName<T>(T data)
        {
            
            NameAttribute nameAttribute = data.GetType().GetCustomAttribute<NameAttribute>();
            return nameAttribute?.Name ?? FormatTemplateName(data.GetType().Name);
        }

        private static KeyValuePair<string, Func<T, TOut>> CreateAttributeKeyValuePair<T, TOut>(string key, Func<T, TOut> accessor) where T : Attribute
        {
            return new KeyValuePair<string, Func<T, TOut>>(key, accessor);
        }

        private static void AddAttributeConstraint<T, TOut>(ICollection<Constraint> constraints, MemberDescriptor property,
            params KeyValuePair<string, Func<T, TOut>>[] attributePropertyMappers) where T : Attribute
        {
            foreach (T attribute in property.Attributes.OfType<T>())
            {
                foreach (KeyValuePair<string, Func<T, TOut>> attributePropertyMapper in attributePropertyMappers)
                {
                    constraints.Add(new Constraint
                    {
                        Name = attributePropertyMapper.Key,
                        Value = attributePropertyMapper.Value(attribute)
                    });
                }
            }
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