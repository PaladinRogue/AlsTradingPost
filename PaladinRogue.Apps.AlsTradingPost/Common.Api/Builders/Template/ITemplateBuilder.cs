using Common.Api.Resources;

namespace Common.Api.Builders.Template
{
    public interface ITemplateBuilder : IBuilder<string, IBuiltResource>
    {
        ITemplateBuilder Create<T>() where T : ITemplate;
        ITemplateBuilder WithTemplateMeta();
    }
}