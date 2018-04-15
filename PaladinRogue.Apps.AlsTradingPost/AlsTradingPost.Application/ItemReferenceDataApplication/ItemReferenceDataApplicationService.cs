using System;
using System.Linq.Expressions;
using AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AutoMapper;
using Common.Resources.Extensions;

namespace AlsTradingPost.Application.ItemReferenceDataApplication
{
    public class ItemReferenceDataApplicationService : IItemReferenceDataApplicationService
    {
        private readonly IItemReferenceDataQueryService _itemReferenceDataQueryService;

        public ItemReferenceDataApplicationService(IItemReferenceDataQueryService itemReferenceDataQueryService)
        {
            _itemReferenceDataQueryService = itemReferenceDataQueryService;
        }

        ItemReferenceDataPagedCollectionAdto IItemReferenceDataApplicationService.Search(ItemReferenceDataSearchAdto itemReferenceDataSearchAdto)
        {
            ItemReferenceDataPagedCollectionDdto result = _itemReferenceDataQueryService.GetPage(
                Mapper.Map<ItemReferenceDataSearchAdto, ItemReferenceDataSearchDdto>(itemReferenceDataSearchAdto),
                    i => string.IsNullOrEmpty(itemReferenceDataSearchAdto.Name) || i.Name.Contains(itemReferenceDataSearchAdto.Name, StringComparison.OrdinalIgnoreCase),
                    itemReferenceDataSearchAdto.OrderBy,
                    itemReferenceDataSearchAdto.OrderByAscending,
                    itemReferenceDataSearchAdto.ThenBy,
                    itemReferenceDataSearchAdto.ThenByAscending
                );

            return Mapper.Map<ItemReferenceDataPagedCollectionDdto, ItemReferenceDataPagedCollectionAdto>(result);
        }
    }
}

