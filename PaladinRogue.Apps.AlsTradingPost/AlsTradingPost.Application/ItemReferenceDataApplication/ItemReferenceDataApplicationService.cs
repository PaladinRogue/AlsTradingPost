using System.Collections.Generic;
using AlsTradingPost.Application.ItemReferenceDataApplication.Interfaces;
using AlsTradingPost.Application.ItemReferenceDataApplication.Models;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AutoMapper;

namespace AlsTradingPost.Application.ItemReferenceDataApplication
{
    public class ItemReferenceDataApplicationService : IItemReferenceDataApplicationService
    {
        private readonly IItemReferenceDataQueryService _itemReferenceDataQueryService;
        private readonly IMapper _mapper;

        public ItemReferenceDataApplicationService(IMapper mapper,
            IItemReferenceDataQueryService itemReferenceDataQueryService)
        {
            _mapper = mapper;
            _itemReferenceDataQueryService = itemReferenceDataQueryService;
        }

        public IList<ItemReferenceDataSummaryAdto> GetAll()
        {
            return _mapper.Map<IList<ItemReferenceDataSummaryProjection>, IList<ItemReferenceDataSummaryAdto>>(_itemReferenceDataQueryService.GetAll());
        }
    }
}

