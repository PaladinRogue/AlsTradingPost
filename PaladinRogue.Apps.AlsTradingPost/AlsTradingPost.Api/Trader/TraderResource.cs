using Common.Api.Concurrency;

namespace AlsTradingPost.Api.Trader
{
    public class TraderResource : VersionedResource
    {
        public string Alias { get; set; }
        public string DCI { get; set; }
    }
}
