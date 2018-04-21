namespace Common.Api.Builders.Resource
{
    public interface ICollectionResourceBuilder : IBuilder<string, object>
    {
        ICollectionResourceBuilder WithMeta(bool extendedMeta = false);
        ICollectionResourceBuilder WithResourceMeta();
        ICollectionResourceBuilder WithSorting();
    }
}