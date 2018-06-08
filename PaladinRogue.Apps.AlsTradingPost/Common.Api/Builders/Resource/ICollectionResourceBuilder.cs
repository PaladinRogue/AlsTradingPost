using System.Collections.Generic;
using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface ICollectionResourceBuilder<TSummaryResource> : IBuilder<IBuiltResource> where TSummaryResource : ISummaryResource
    {
        ICollectionResourceBuilder<TSummaryResource> Create(ICollectionResource<TSummaryResource> resource, ITemplate template);
        ICollectionResourceBuilder<TSummaryResource> WithResourceMeta();
    }
}