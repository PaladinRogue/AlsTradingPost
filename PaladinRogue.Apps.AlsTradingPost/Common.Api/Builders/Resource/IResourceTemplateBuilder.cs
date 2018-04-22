using Microsoft.AspNetCore.Http;

namespace Common.Api.Builders.Resource
{
    public interface IResourceTemplateBuilder : IBuilder<string, object>
    {
        IResourceTemplateBuilder WithTemplateMeta();
        IResourceTemplateBuilder WithResourceMeta();
    }
}