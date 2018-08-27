using System.Collections.Generic;
using Common.Application.Pagination.Interfaces;
using Common.Application.Sorting;
using Common.Resources.Sorting;

namespace AlsTradingPost.Application.MagicItemTemplate.Models
{
    public class MagicItemTemplateSearchAdto : IPaginationAdto, ISortAdto
    {
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
        public IList<SortBy> Sort { get; set; }
    }
}
