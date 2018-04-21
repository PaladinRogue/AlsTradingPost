namespace Common.Api.Builders.Resource
{
    public interface ICollectionResourceBuilder : IBuilder<string, object>
    {
        ICollectionResourceBuilder WithTemplateMeta();
        ICollectionResourceBuilder WithResourceMeta();
        ICollectionResourceBuilder WithSummaryResourceMeta();
        ICollectionResourceBuilder WithSorting();
    }
}