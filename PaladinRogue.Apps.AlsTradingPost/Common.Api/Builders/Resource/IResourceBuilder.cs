namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder : IBuilder<string, object>
    {
        IResourceBuilder WithMeta(bool extendedMeta = false);
        IResourceBuilder WithResourceMeta();
    }
}