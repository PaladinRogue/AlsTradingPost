using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.Builders.Resource;
using Common.Api.Resources;
using Common.Api.Sorting;
using Common.Api.Validation;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;
using ReadOnlyAttribute = Common.Api.Resources.ReadOnlyAttribute;

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
        
        public static IList<ResourceBuilderResource<T>> BuildCollectionResourceData<T>(ICollectionResource<T> data) where T : ISummaryResource
        {
            return data.Results.Select(BuildResource).ToList();
        }

        public static Meta BuildMeta<T>(T data)
        {
            return new Meta
            {
                TemplateTypeName = GetTemplateTypeName(data),
                Properties = new List<PropertyMeta>()
            };
        }
        
        public static void BuildValidationMeta<T>(Meta meta, T data)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data.GetType()))
            {
                IList<Constraint> constraints = new List<Constraint> {
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
                
                AddOrUpdatePropertyConstraints(meta, property.Name, constraints);
            }
        }
        
        public static void BuildFieldMeta<T>(Meta meta, T data)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data.GetType()))
            {
                if (!FieldTypeMapper.HasFieldType(property.PropertyType)) continue;
                
                IList<Constraint> constraints = new List<Constraint> {
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
                
                AddOrUpdatePropertyConstraints(meta, property.Name, constraints);
            }
        }

        public static void BuildSortingMeta<T, TSummaryResource>(Meta meta, T data)
            where TSummaryResource : ISummaryResource
        {
            IList<string> sortableFields = (
                from property in typeof(TSummaryResource).GetProperties()
                let sortableAttribute = property.GetCustomAttribute<SortableAttribute>()
                where sortableAttribute != null
                select property.Name.ToCamelCase()).ToList();


            if (data is IThenByTemplate thenByTemplate)
            {
                IEnumerable<string> thenByFields = sortableFields.Where(x => x != thenByTemplate.OrderBy);
                
                if (thenByFields.Any())
                {
                    AddOrUpdatePropertyConstraint(meta, nameof(thenByTemplate.ThenBy), new Constraint
                    {
                        Name = FieldMeta.Values,
                        Value = thenByFields
                    });
                }
            }

            if (data is IOrderByTemplate orderByTemplate)
            {
                if (sortableFields.Any())
                {
                    AddOrUpdatePropertyConstraint(meta, nameof(orderByTemplate.OrderBy), new Constraint
                    {
                        Name = FieldMeta.Values,
                        Value = sortableFields
                    });
                }
            }
        }

        public static IList<Link> BuildLinks<T>(T data)
        {
            return new List<Link>();
        }

        private static ResourceBuilderResource<T> BuildResource<T>(T resourceData)
        {
            return new ResourceBuilderResource<T>
            {
                Data = BuildResourceData(resourceData),
                Meta = BuildMeta(resourceData),
                Links = BuildLinks(resourceData)
            };
        }

        private static void AddOrUpdatePropertyConstraints(Meta meta, string propertyName, IList<Constraint> constraints)
        {
            PropertyMeta propertyMeta = meta.Properties.SingleOrDefault(p => p.Name == propertyName);
            if (propertyMeta == null)
            {
                meta.Properties.Add(new PropertyMeta
                {
                    Name = propertyName,
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

        private static void AddOrUpdatePropertyConstraint(Meta meta, string propertyName, Constraint constraint)
        {
            PropertyMeta propertyMeta = meta.Properties.SingleOrDefault(p => p.Name == propertyName);
            if (propertyMeta == null)
            {
                meta.Properties.Add(new PropertyMeta
                {
                    Name = propertyName,
                    Constraints = new List<Constraint>
                    {
                        constraint
                    }
                });
            }
            else
            {
                propertyMeta.Constraints.Add(constraint);
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