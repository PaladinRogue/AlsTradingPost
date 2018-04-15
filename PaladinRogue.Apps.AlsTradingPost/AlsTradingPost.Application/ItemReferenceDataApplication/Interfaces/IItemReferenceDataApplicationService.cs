using AlsTradingPost.Application.ItemReferenceDataApplication.Models;

namespace AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces
{
    public interface IItemReferenceDataApplicationService
    {
        ItemReferenceDataPagedCollectionAdto Search(ItemReferenceDataSearchAdto itemReferenceDataSearchAdto);
    }
}
