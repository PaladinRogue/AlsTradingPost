namespace Common.Api.Builders
{
    public class Data<T>
    {
        public string TemplateTypeName { get; set; }

        public T Resource { get; set; }
    }
}