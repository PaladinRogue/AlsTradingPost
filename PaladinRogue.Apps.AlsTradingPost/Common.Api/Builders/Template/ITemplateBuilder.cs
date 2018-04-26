using Common.Api.Resources;

namespace Common.Api.Builders.Template
{
    public interface ITemplateBuilder : IBuilder<string, object>
    {
        ITemplateBuilder Create<T>() where T : ITemplate;
        ITemplateBuilder WithTemplateMeta();
    }
}