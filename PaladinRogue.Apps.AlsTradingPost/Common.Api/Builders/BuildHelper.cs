using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Common.Api.Builders.Dictionary;
using Common.Api.Builders.Resource;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Resources;
using Common.Api.Sorting;
using Common.Api.Validation;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;
using ReadOnlyAttribute = Common.Api.Resources.ReadOnlyAttribute;

namespace Common.Api.Builders
{
    public class BuildHelper : IBuildHelper
    {
        private readonly ILinkBuilder _linkBuilder;
        
        public BuildHelper(ILinkBuilder linkBuilder)
        {
            _linkBuilder = linkBuilder;
        }
        
        public IList<ResourceBuilderResource<TSummaryResource>> BuildCollectionResourceData<TSummaryResource>(ICollectionResource<TSummaryResource> data)
            where TSummaryResource : ISummaryResource
        {
            return data.Results.Select(BuildResourceBuilder).ToList();
        }

        public ResourceBuilderResource<T> BuildResourceBuilder<T>(T data)
        {
            return new ResourceBuilderResource<T>
            {
                Data = BuildData(data),
                Meta = BuildMeta(data),
                Links = _linkBuilder.BuildLinks(data)
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

        public static void BuildSortingMeta<T>(Meta meta, T data, Type summaryResourceType)
            where T : ITemplate
        {
            IList<string> sortableFields = (
                from property in summaryResourceType.GetProperties()
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

        public static void AddSearchQueryParams<T>(IEnumerable<Link> links, T data)
        {
            AddQueryParams(links.Single(l => l.Name == LinkType.Search), data);
        }

        public static void AddSelfQueryParams<T>(IEnumerable<Link> links, T data)
        {
            AddQueryParams(links.Single(l => l.Name == LinkType.Self), data);
        }

        public static void AddPagingLinks<T, TSummaryResource>(ResourceBuilderResource<T> resource, IPagedCollectionResource<TSummaryResource> data, IPaginationTemplate pagingData) 
            where TSummaryResource : ISummaryResource
        {
            Link selfLink = resource.Links.Single(l => l.Name == LinkType.Self);
            
            foreach (Link pagingLink in PagingLinkHelper.GetPagingLinks(pagingData, data.TotalResults))
            {
                pagingLink.Uri = selfLink.Uri;
                resource.Links.Add(pagingLink);
            }
        }

        public static IDictionary<string, object> Build<T>(ResourceBuilderResource<T> resource)
        {
            return DictionaryBuilder<string, object>.Create()
                .Add(resource.Data.TypeName, DictionaryBuilder<string, object>.Create()
                    .Add(ResourceType.Data, resource.Data.Resource)
                    .Add(ResourceType.Meta, resource.Meta.Properties.BuildPropertyDictionary())
                    .Add(ResourceType.Links, resource.Links.BuildLinkDictionary())
                    .Build())
                .Build();
        }
        
        private static Data<T> BuildData<T>(T resourceData)
        {
            return new Data<T>
            {
                TypeName = GetConventionTypeName(resourceData),
                Resource = resourceData
            };
        }

        private static Meta BuildMeta<T>(T data)
        {
            return new Meta
            {
                TemplateTypeName = GetConventionTypeName(data),
                Properties = new List<PropertyMeta>()
            };
        }

        private static void AddQueryParams<T>(Link link, T data)
        {
            if (link == null)
            {
                return;
            }
            
            link.QueryParams = data.GetType().GetProperties()
                .Where(p => p.GetValue(data) != null)
                .ToDictionary(
                    p => p.Name.ToCamelCase(),
                    p => p.GetValue(data).ToString().ToCamelCase()
                );
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
        
        private static string FormatConventionName(string resourceName)
        {
            return resourceName.Replace("Template", string.Empty).Replace("Resource", string.Empty);
        }
    }
}