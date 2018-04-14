using System.Collections.Generic;
using System.Linq;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain
{
    public class ItemReferenceDataQueryService : IItemReferenceDataQueryService
    {
        private readonly IMapper _mapper;
        private readonly IItemReferenceDataRepository _itemReferenceDataRepository;

        public ItemReferenceDataQueryService(IMapper mapper, IItemReferenceDataRepository itemReferenceDataRepository)
        {
            _itemReferenceDataRepository = itemReferenceDataRepository;
            _mapper = mapper;
        }

        public IList<ItemReferenceDataSummaryProjection> GetAll()
        {
            return _mapper.Map<IList<Domain.Models.ItemReferenceData>, IList<ItemReferenceDataSummaryProjection>>(_itemReferenceDataRepository.Get().ToList<Domain.Models.ItemReferenceData>());
        }
    }
}
