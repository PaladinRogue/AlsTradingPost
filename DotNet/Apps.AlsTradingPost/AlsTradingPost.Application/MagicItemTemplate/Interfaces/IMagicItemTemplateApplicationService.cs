using AlsTradingPost.Application.MagicItemTemplate.Models;

namespace AlsTradingPost.Application.MagicItemTemplate.Interfaces
{
    public interface IMagicItemTemplateApplicationService
    {
        MagicItemTemplatePagedCollectionAdto Search(MagicItemTemplateSearchAdto magicItemTemplateSearchAdto);
    }
}
