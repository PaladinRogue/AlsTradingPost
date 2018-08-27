using Common.Domain.Pagination;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain.Models
{
    public class MagicItemTemplateSearchDdto : PaginationDdto
    {
        public string Name { get; set; }
    }
}
