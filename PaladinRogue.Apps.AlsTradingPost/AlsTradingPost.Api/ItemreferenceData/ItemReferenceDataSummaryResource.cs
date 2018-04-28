using System;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace AlsTradingPost.Api.ItemReferenceData
{
    [SelfLink(RouteDictionary.ItemReferenceDataGetById, HttpVerbs.Get)]
    public class ItemReferenceDataSummaryResource : ISummaryResource
    {
        [Hidden]
        [ReadOnly]
        public Guid Id { get; set; }
        
        [Sortable]
        [ReadOnly]
        public string Name { get; set; }
    }
}
