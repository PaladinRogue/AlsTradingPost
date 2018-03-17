using Common.Api.Request;

namespace AlsTradingPost.Api.Request.Admin
{
    public class UpdateAdminRequestDto : VersionedRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}