using AlsTradingPost.Domain.MagicItemTemplateDomain.Models;
using AlsTradingPost.Domain.Models;
using Common.Domain.Services.Domain;

namespace AlsTradingPost.Domain.MagicItemTemplateDomain.Interfaces
{
    public interface IMagicItemTemplateDomainService : IGetPageService<MagicItemTemplate, MagicItemTemplatePagedCollectionDdto>
    {
    }
}
