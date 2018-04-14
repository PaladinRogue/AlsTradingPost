using System.Collections.Generic;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;

namespace AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces
{
    public interface IItemReferenceDataApplicationService
    {
        IList<ItemReferenceDataSummaryAdto> GetAll();
    }
}
