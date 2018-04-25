namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder<in T> : IBuilder<string, object>
    {
        IResourceBuilder<T> Create(T resource);
        IResourceBuilder<T> WithResourceMeta();
    }
}