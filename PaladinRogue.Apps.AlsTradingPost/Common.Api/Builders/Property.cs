namespace Common.Api.Builders
{
    public class Property<T>
    {
        public string Name { get; set; }

        public T Value { get; set; }
    }
}