using System;
using Common.Api.Constants;
using Common.Api.Links;
using Common.Api.Resources;

namespace AlsTradingPost.Api.ItemReferenceData
{
    [SelfLink(RouteDictionary.ItemReferenceData, HttpVerbs.Get)]
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
