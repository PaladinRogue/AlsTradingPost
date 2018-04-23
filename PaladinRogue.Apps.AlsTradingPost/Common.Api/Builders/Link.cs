namespace Common.Api.Builders
{
    public class Link
    {
        public string Name { get; set; }
        
        public string Uri { get; set; }
        
        public string[] AllowVerbs { get; set; }
    }
}