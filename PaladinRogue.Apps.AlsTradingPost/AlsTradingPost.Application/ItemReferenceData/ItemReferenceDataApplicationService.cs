using AlsTradingPost.Application.ItemReferenceData.Interfaces;
using AlsTradingPost.Application.ItemReferenceData.Models;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AutoMapper;
using Common.Application.Validation;
using FluentValidation;

namespace AlsTradingPost.Application.ItemReferenceData
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

