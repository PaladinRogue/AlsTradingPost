namespace Common.Api.ResourceFormatter.Resources
{
    public class Resource
    {
        public Resource(dynamic data)
        {
            Data = data;
            Meta = new Meta(data);
        }

        public dynamic Data { get; set; }
        public Meta Meta { get; set; }
    }
}