using Common.Api.Links;
using Common.Api.Pagination;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.ItemReferenceData
{
    [SelfLink(RouteDictionary.ItemReferenceDataGet, HttpVerbs.Get)]
    public class ItemReferenceDataPagedCollectionResource : PagedCollectionResource<ItemReferenceDataSummaryResource>
    {
    }
}