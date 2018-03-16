using Common.Api.Resource;

namespace AlsTradingPost.Api.Resources.Admin
{
    public class AdminResource : VersionedResource
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
