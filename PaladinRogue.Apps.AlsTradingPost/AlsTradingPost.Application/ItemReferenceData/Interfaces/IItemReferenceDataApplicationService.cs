using AlsTradingPost.Application.ItemReferenceData.Models;

namespace AlsTradingPost.Application.ItemReferenceData.Interfaces
{
    public interface IItemReferenceDataApplicationService
    {
        ItemReferenceDataPagedCollectionAdto Search(ItemReferenceDataSearchAdto itemReferenceDataSearchAdto);
    }
}
