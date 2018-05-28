using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Trader
{
    [SelfLink(RouteDictionary.TraderById, HttpVerbs.Get, HttpVerbs.Put)]
    public class TraderResource : VersionedResource
    {
        [MaxLength(50)]
        [Required]
        public string Alias { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string DCI { get; set; }
    }
}
