using System.Collections.Generic;
using Common.Api.Builders.Resource;
using Common.Api.Resources;

namespace Common.Api.Builders
{
    public interface IBuildHelper
    {
        IList<ResourceBuilderResource<TSummaryResource>> BuildCollectionResourceData<TSummaryResource>(
            ICollectionResource<TSummaryResource> data) where TSummaryResource : ISummaryResource;

        ResourceBuilderResource<T> BuildResourceBuilder<T>(T resourceData);
    }
}