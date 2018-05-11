using Common.Domain.Concurrency;

namespace AlsTradingPost.Domain.TraderDomain
{
    public class RegisteredTraderProjection : VersionedProjection
    {
        public string Alias { get; set; }
        
        public string DCI { get; set; }
    }
}