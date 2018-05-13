using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Interfaces;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces
{
    public interface IMagicItemTemplateQueryService : IPagedSummaryQueryService<MagicItemTemplate, MagicItemTemplatePagedCollectionDdto>
    {
    }
}
