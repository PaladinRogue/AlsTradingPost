using Common.Application.Pagination.Interfaces;
using Common.Application.Sorting;

namespace AlsTradingPost.Application.ItemReferenceDataApplication.Models
{
    public class ItemReferenceDataSearchAdto : IPaginationAdto, IThenByAdto
    {
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageOffset { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByAscending { get; set; }
        public string ThenBy { get; set; }
        public bool? ThenByAscending { get; set; }
    }
}
