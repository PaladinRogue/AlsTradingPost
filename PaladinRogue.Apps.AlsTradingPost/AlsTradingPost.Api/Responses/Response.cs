namespace AlsTradingPost.Api.Responses
{
    public class Response
    {
        public Response(dynamic data)
        {
            Data = data;
            Meta = new Meta(data);
        }

        public dynamic Data { get; set; }
        public Meta Meta { get; set; }
    }
}