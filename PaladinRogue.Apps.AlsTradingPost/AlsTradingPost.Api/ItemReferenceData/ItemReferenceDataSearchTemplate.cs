﻿using Common.Api.Constants;
using Common.Api.Links;
using Common.Api.Pagination.Interfaces;
using Common.Api.Sorting;
using Common.Api.Validation.Attributes;
using Common.Resources.Extensions;

namespace AlsTradingPost.Api.ItemReferenceData
{
    [SelfLink(RouteDictionary.ItemReferenceDataSearchTemplate, HttpVerbs.Get)]
    [SearchLink(RouteDictionary.ItemReferenceDataGet)]
    public class ItemReferenceDataSearchTemplate : IPaginationTemplate, IThenByTemplate
    {
        public ItemReferenceDataSearchTemplate()
        {
            PageOffset = 0;
            PageSize = 10;
            OrderBy = nameof(Name).ToCamelCase();
            OrderByAscending = true;
        }
        
        [Length(3, 50)]
        public string Name { get; set; }
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAscending { get; set; }
        public string ThenBy { get; set; }
        public bool? ThenByAscending { get; set; }
    }
}
