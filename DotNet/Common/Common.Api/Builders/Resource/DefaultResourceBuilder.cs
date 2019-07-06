using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Api.Concurrency.Interfaces;
using Common.Api.Links;
using Common.Api.Meta;
using Common.Api.Pagination.Interfaces;
using Common.Api.PropertyTypes;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Api.Builders.Resource
{
    public class DefaultResourceBuilder : IResourceBuilder
    {
        private readonly ILinkBuilder _linkBuilder;

        public DefaultResourceBuilder(ILinkBuilder linkBuilder)
        {
            _linkBuilder = linkBuilder;
        }

        private static string GetFieldType(PropertyInfo propertyInfo)
        {
            if (propertyInfo.GetCustomAttribute<EmailAddressAttribute>() != null)
            {
                return FieldType.Email;
            }

            if (propertyInfo.GetCustomAttribute<PasswordAttribute>() != null)
            {
                return FieldType.Password;
            }

            return null;
        }

        public BuiltResource Build<TResource>(TResource resource) where TResource: IResource
        {
            Type resourceType = resource.GetType();
            IEnumerable<PropertyInfo> properties = resourceType.GetProperties().Where(p =>
                p.Name != nameof(IEntityResource.Id) && p.Name != nameof(IVersionedResource.Version) );

            return new BuiltResource
            {
                Id = resource is IEntityResource entityResource ? entityResource.Id : (Guid?)null,
                Type = resourceType,
                Properties = properties.Where(p => !p.GetCustomAttributes<IgnoreAttribute>().Any()).Select(p => new Property
                {
                    Type = p.PropertyType,
                    FieldType = GetFieldType(p),
                    Name = p.Name,
                    Value = p.GetValue(resource),
                    Constraints = new Constraints
                    {
                        IsReadonly = p.GetCustomAttribute<ReadOnlyAttribute>()?.IsReadOnly,
                        IsSortable = p.GetCustomAttribute<SortableAttribute>()?.IsSortable,
                        IsHidden = p.GetCustomAttribute<HiddenAttribute>()?.IsHidden
                    },
                    ValidationConstraints = new ValidationConstraints
                    {
                        IsRequired = p.GetCustomAttribute<RequiredAttribute>()?.IsRequired,
                        MinLenth = p.GetCustomAttribute<LengthAttribute>()?.MinLength ?? p.GetCustomAttribute<MinLengthAttribute>()?.MinLength,
                        MaxLength = p.GetCustomAttribute<LengthAttribute>()?.MaxLength ?? p.GetCustomAttribute<MaxLengthAttribute>()?.MaxLength
                    }
                }),
                Links = _linkBuilder.BuildLinks(resource),
                Version = resource is IVersioned<IConcurrencyVersion> versionedResource ? versionedResource.Version : null
            };
        }

        public BuiltCollectionResource BuildCollection<T>(ICollectionResource<T> collectionResource) where T : IResource
        {
            BuiltCollectionResource builtCollectionResource = new BuiltCollectionResource
            {
                TotalResults = collectionResource.Results.Count,
                BuiltResources = collectionResource.Results.Select(Build),
                Links = _linkBuilder.BuildLinks(collectionResource)
            };

            return builtCollectionResource;
        }

        public BuiltCollectionResource BuildCollection<T>(ICollectionResource<T> collectionResource, ITemplate template) where T: IResource
        {
            BuiltCollectionResource builtCollectionResource = new BuiltCollectionResource
            {
                TotalResults = collectionResource.Results.Count,
                BuiltResources = collectionResource.Results.Select(Build),
                Links = _linkBuilder.BuildLinks(collectionResource, template)
            };

            return builtCollectionResource;
        }

        public BuiltCollectionResource BuildCollection<T>(IPagedCollectionResource<T> pagedCollectionResource, IPaginationTemplate paginationTemplate) where T : IResource
        {
            BuiltCollectionResource builtCollectionResource = new BuiltCollectionResource
            {
                TotalResults = pagedCollectionResource.TotalResults,
                BuiltResources = pagedCollectionResource.Results.Select(Build),
                Links = _linkBuilder.BuildLinks(pagedCollectionResource, paginationTemplate)
            };

            return builtCollectionResource;
        }
    }
}
