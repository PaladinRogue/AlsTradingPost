using System;
using Common.Api.Builders.Resource.Attributes;
using Common.Api.Resources;

namespace AlsTradingPost.Api.ItemReferenceData
{
    public class ItemReferenceDataSummaryResource : ISummaryResource
    {
        public Guid Id { get; set; }
        [Sortable]
        public string Name { get; set; }
    }
}
