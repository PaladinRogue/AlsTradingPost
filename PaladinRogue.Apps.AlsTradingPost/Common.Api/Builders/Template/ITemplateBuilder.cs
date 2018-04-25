namespace Common.Api.Builders.Template
{
    public interface ITemplateBuilder<in T> : IBuilder<string, object>
    {
        ITemplateBuilder<T> Create();
        ITemplateBuilder<T> WithTemplateMeta();
    }
}