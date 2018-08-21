using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.Trader
{
    [SelfLink(RouteDictionary.TraderRegisterResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.TraderRegister)]
    public class TraderTemplate : ITemplate
    {
        [MaxLength(50)]
        [Required]
        public string Alias { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string DCI { get; set; }
    }
}
