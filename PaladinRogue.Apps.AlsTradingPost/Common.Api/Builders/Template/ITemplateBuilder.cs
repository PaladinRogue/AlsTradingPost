using Common.Api.Resources;

namespace Common.Api.Builders.Template
{
    public interface ITemplateBuilder : IBuilder<IBuiltResource>
    {
        ITemplateBuilder Create<T>() where T : ITemplate;
        ITemplateBuilder WithTemplateMeta();
        ITemplateBuilder WithSorting<T>() where T : ISummaryResource;
    }
}