using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface IResourceBuilder : IBuilder<IBuiltResource>
    {
        IResourceBuilder Create(IResource resource);
        IResourceBuilder WithResourceMeta();
    }
}