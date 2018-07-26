using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.Builders;
using Common.Api.Headers;
using Common.Api.Resources;
using Common.Api.Sorting;
using Common.Api.Validation;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;
using Microsoft.AspNetCore.Http;

namespace Common.Api.Meta
{
    public class MetaBuilder : IMetaBuilder
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MetaBuilder(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Meta BuildMeta<T>(T data)
        {
            Meta meta = new Meta
            {
                TemplateTypeName = GetConventionTypeName(data),
                Properties = new List<PropertyMeta>()
            };
            
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data.GetType()))
            {
                if (!FieldTypeMapper.HasFieldType(property.PropertyType)) continue;
                
                IList<Constraint> constraints = new List<Constraint>
                {
                    new Constraint
                    {
                        Name = FieldMeta.Type,
                        Value = FieldTypeMapper.GetFieldType(property.PropertyType)
                    }
                };
                
                AddOrUpdatePropertyConstraints(meta, property.Name, constraints);
            }

            return meta;
        }
        
        public void BuildValidationMeta<T>(Meta meta, T data)
        {
            if(!_httpContextAccessor.HasRequestHeader(HeaderType.ExtendedMeta)) return;
            
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data.GetType()))
            {
                IList<Constraint> constraints = new List<Constraint>();
                
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
        
        public void BuildFieldMeta<T>(Meta meta, T data)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data.GetType()))
            {
                IList<Constraint> constraints = new List<Constraint>();
                    
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
        
        public void BuildSortingMeta<T>(Meta meta, T data, Type summaryResourceType)
            where T : ITemplate
        {
            if(!_httpContextAccessor.HasRequestHeader(HeaderType.ExtendedMeta)) return;
            
            IList<string> sortableFields = (
                from property in summaryResourceType.GetProperties()
                let sortableAttribute = property.GetCustomAttribute<SortableAttribute>()
                where sortableAttribute != null
                select property.Name.ToCamelCase()).ToList();

            if (!(data is ISortTemplate sortTemplate)) return;

            if (sortableFields.Any())
            {
                AddOrUpdatePropertyConstraint(meta, nameof(sortTemplate.Sort), new Constraint
                {
                    Name = FieldMeta.Values,
                    Value = sortableFields
                });
            }
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
        
        private static string GetConventionTypeName<T>(T data)
        {
            NameAttribute nameAttribute = data.GetType().GetCustomAttribute<NameAttribute>();
            return nameAttribute?.Name ?? FormatConventionName(data.GetType().Name);
        }
        
        private static string FormatConventionName(string resourceName)
        {
            return resourceName.Replace("Template", string.Empty).Replace("Resource", string.Empty);
        }
    }
}