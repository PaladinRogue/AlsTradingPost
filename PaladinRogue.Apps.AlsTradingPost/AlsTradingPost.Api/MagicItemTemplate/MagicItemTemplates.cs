using AlsTradingPost.Setup.Infrastructure.Routing;
using Common.Api.Links;
using Common.Api.Pagination;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.MagicItemTemplate
{
    [SelfLink(RouteDictionary.MagicItemTemplateGet, HttpVerb.Get)]
    public class MagicItemTemplates : PagedCollectionResource<MagicItemTemplateSummaryResource>
    {
    }
}