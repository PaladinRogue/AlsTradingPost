using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Api.Builders.Resource
{
    public class ResourceBuilder<T, TTemplate> : IResourceBuilder
    {
        protected readonly ResourceBuilderResource<T> Resource;

        protected readonly T ResourceData;
        protected readonly TTemplate Template;

        protected ResourceBuilder(T resource, TTemplate template)
        {
            ResourceData = resource;
            Template = template;

            Resource = new ResourceBuilderResource<T>
            {
                Meta = BuildHelper.BuildMeta(template),
                Data = BuildHelper.BuildResourceData(resource)
            };
        }

        public static IResourceBuilder Create(T resource, TTemplate template)
        {
            return new ResourceBuilder<T, TTemplate>(resource, template);
        }

        public IResourceBuilder WithResourceMeta()
        {
            if (Resource.Meta == null)
            {
                throw new ArgumentException("You must build the meta before adding sorting");
            }

            BuildHelper.AddFieldMeta(Resource.Meta, ResourceData);

            return this;
        }

        public IDictionary<string, object> Build()
        {
            return new Dictionary<string, object>
            {
                {
                    ResourceType.Data, new Dictionary<string, object>
                    {
                        {Resource.Data.TypeName, Resource.Data.Resource}
                    }
                },
                {
                    ResourceType.Meta, new Dictionary<string, object>
                    {
                        {
                            Resource.Meta.TemplateTypeName, Resource.Meta.Properties.ToDictionary(
                                p => p.Name,
                                p => p.Constraints.ToDictionary(
                                    c => c.Name,
                                    c => c.Value
                                ))
                        }
                    }
                }
            };
        }
    }

    public interface IResourceBuilder
    {
        IResourceBuilder WithResourceMeta();
        IDictionary<string, object> Build();
    }
}