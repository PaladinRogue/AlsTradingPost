using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.Builders.Attributes;
using Common.Api.Builders.Resource;
using Common.Api.Builders.Resource.Attributes;
using Common.Api.Builders.Template.Attributes;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Api.Sorting;
using Common.Api.Validation;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;

namespace Common.Api.Builders
{
    public static class BuildHelper
    {
        public static Data<T> BuildTemplateData<T>(T templateData)
        {
            return new Data<T>
            {
                TypeName = GetTemplateTypeName(templateData),
                Resource = templateData
            };
        }
        
        public static Data<T> BuildResourceData<T>(T resourceData)
        {
            return new Data<T>
            {
                TypeName = GetResourceTypeName(resourceData),
                Resource = resourceData
            };
        }
        
        public static IList<ResourceBuilderResource<T>> BuildCollectionResourceData<T>(IPagedCollectionResource<T> resourceData) where T : ISummaryResource
        {
            List<ResourceBuilderResource<T>> results = new List<ResourceBuilderResource<T>>();
            
            foreach (T resourceDataResult in resourceData.Results)
            {
                
            }

            return results;
        }

        public static Meta BuildMeta<T>(T templateData)
        {
            IList<PropertyMeta> properties = new List<PropertyMeta>();

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
                
                properties.Add(new PropertyMeta
                {
                    Name = property.Name,
                    Constraints = constraints
                });
            }
            
            Meta meta =  new Meta
            {
                TemplateTypeName = GetTemplateTypeName(templateData),
                Properties = properties
            };

            AddFieldMeta(meta, templateData);

            return meta;
        }
        
        public static void AddFieldMeta<T>(Meta resourceMeta, T resourceData)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(resourceData.GetType()))
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
                    CreateAttributeKeyValuePair<HiddenAttribute, bool>(
                        FieldMeta.Hidden,
                        a => a.IsHidden
                    ));

                AddAttributeConstraint(constraints, property,
                    CreateAttributeKeyValuePair<ReadOnlyAttribute, bool>(
                        FieldMeta.ReadOnly,
                        a => a.IsReadOnly
                    ));
                
                PropertyMeta propertyMeta = resourceMeta.Properties.SingleOrDefault();
                if (propertyMeta == null)
                {
                    resourceMeta.Properties.Add(new PropertyMeta
                    {
                        Name = property.Name,
                        Constraints = constraints
                    });
                }
                else
                {
                    foreach (Constraint constraint in constraints)
                    {
                        propertyMeta.Constraints.Add(constraint);
                    }
                }
            }
        }

        public static void AddSorting<T, TSummaryResource>(Meta meta, T templateData)
            where TSummaryResource : ISummaryResource
        {
            IList<string> sortableFields = (
                from property in typeof(TSummaryResource).GetProperties()
                let sortableAttribute = property.GetCustomAttribute<SortableAttribute>()
                where sortableAttribute != null
                select property.Name.ToCamelCase()).ToList();


            if (templateData is IThenByTemplate thenByTemplate)
            {
                IEnumerable<string> thenByFields = sortableFields.Where(x => x != thenByTemplate.OrderBy);
                
                if (thenByFields.Any())
                {
                    meta.Properties.Single(p => p.Name == nameof(thenByTemplate.ThenBy)).Constraints.Add(new Constraint
                    {
                        Name = FieldMeta.Values,
                        Value = thenByFields
                    });
                }
            }

            if (templateData is IOrderByTemplate orderByTemplate)
            {
                if (sortableFields.Any())
                {
                    meta.Properties.Single(p => p.Name == nameof(orderByTemplate.OrderBy)).Constraints.Add(
                        new Constraint
                        {
                            Name = FieldMeta.Values,
                            Value = sortableFields
                        });
                }
            }
        }

        private static string GetTemplateTypeName<T>(T data)
        {
            NameAttribute nameAttribute = data.GetType().GetCustomAttribute<NameAttribute>();
            return nameAttribute?.Name ?? FormatTemplateName(data.GetType().Name);
        }
        
        private static string GetResourceTypeName<T>(T data)
        {
            NameAttribute nameAttribute = data.GetType().GetCustomAttribute<NameAttribute>();
            return nameAttribute?.Name ?? FormatResourceName(data.GetType().Name);
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