using Common.Domain.Models;

namespace AlsTradingPost.Domain.AdminServices.Models
{
    public class AdminSummaryProjection : VersionedProjection
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
