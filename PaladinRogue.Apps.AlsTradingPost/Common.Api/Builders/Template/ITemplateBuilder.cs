namespace Common.Api.Builders.Template
{
    public interface ITemplateBuilder : IBuilder<string, object>
    {
        ITemplateBuilder WithMeta();
    }
}