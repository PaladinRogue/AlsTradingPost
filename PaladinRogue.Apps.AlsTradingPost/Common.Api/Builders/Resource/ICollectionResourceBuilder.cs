using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface ICollectionResourceBuilder<TSummaryResource> : IBuilder<string, object> where TSummaryResource : ISummaryResource
    {
        ICollectionResourceBuilder<TSummaryResource> Create(ICollectionResource<TSummaryResource> resource, ITemplate template);
        ICollectionResourceBuilder<TSummaryResource> WithTemplateMeta();
        ICollectionResourceBuilder<TSummaryResource> WithResourceMeta();
        ICollectionResourceBuilder<TSummaryResource> WithSummaryResourceMeta();
        ICollectionResourceBuilder<TSummaryResource> WithSorting();
    }
}