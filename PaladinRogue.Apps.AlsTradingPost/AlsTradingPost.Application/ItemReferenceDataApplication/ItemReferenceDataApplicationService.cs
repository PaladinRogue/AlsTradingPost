using System;
using AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AutoMapper;
using Common.Application.Validation;
using Common.Resources.Extensions;
using FluentValidation;

namespace AlsTradingPost.Application.ItemReferenceDataApplication
{
    public class ItemReferenceDataApplicationService : IItemReferenceDataApplicationService
    {
        private readonly IItemReferenceDataQueryService _itemReferenceDataQueryService;
        private readonly IValidator<ItemReferenceDataSearchAdto> _itemReferenceDataSearchAdtoValidator;

        public ItemReferenceDataApplicationService(IItemReferenceDataQueryService itemReferenceDataQueryService,
            IValidator<ItemReferenceDataSearchAdto> itemReferenceDataSearchAdtoValidator)
        {
            _itemReferenceDataQueryService = itemReferenceDataQueryService;
            _itemReferenceDataSearchAdtoValidator = itemReferenceDataSearchAdtoValidator;
        }

        ItemReferenceDataPagedCollectionAdto IItemReferenceDataApplicationService.Search(ItemReferenceDataSearchAdto itemReferenceDataSearchAdto)
        {
            _itemReferenceDataSearchAdtoValidator.ValidateAndThrow(itemReferenceDataSearchAdto);
            
            ItemReferenceDataPagedCollectionDdto result = _itemReferenceDataQueryService.GetPage(
                Mapper.Map<ItemReferenceDataSearchAdto, ItemReferenceDataSearchDdto>(itemReferenceDataSearchAdto),
                    i => string.IsNullOrEmpty(itemReferenceDataSearchAdto.Name) || i.Name.Contains(itemReferenceDataSearchAdto.Name),
                    itemReferenceDataSearchAdto.OrderBy,
                    itemReferenceDataSearchAdto.OrderByAscending,
                    itemReferenceDataSearchAdto.ThenBy,
                    itemReferenceDataSearchAdto.ThenByAscending
                );

            return Mapper.Map<ItemReferenceDataPagedCollectionDdto, ItemReferenceDataPagedCollectionAdto>(result);
        }
    }
}

