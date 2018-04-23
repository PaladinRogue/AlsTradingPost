using Common.Api.Constants;
using Common.Api.Links;
using Common.Api.Pagination;

namespace AlsTradingPost.Api.ItemReferenceData
{
    [SelfLink(RouteDictionary.ItemReferenceDataGet, HttpVerbs.Get)]
    public class ItemReferenceDataPagedCollectionResource : PagedCollectionResource<ItemReferenceDataSummaryResource>
    {
    }
}