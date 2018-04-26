using Common.Api.Resources;

namespace Common.Api.Builders.Resource
{
    public interface IResourceTemplateBuilder : IBuilder<string, object>
    {
        IResourceTemplateBuilder Create(IResource resource, ITemplate template);
        IResourceTemplateBuilder WithTemplateMeta();
        IResourceTemplateBuilder WithResourceMeta();
    }
}