namespace Common.Api.Builders
{
    public class Data<T>
    {
        public string TypeName { get; set; }

        public T Resource { get; set; }
    }
}