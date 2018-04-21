namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder : IBuilder<string, object>
    {
        IResourceBuilder WithTemplateMeta();
        IResourceBuilder WithResourceMeta();
    }
}