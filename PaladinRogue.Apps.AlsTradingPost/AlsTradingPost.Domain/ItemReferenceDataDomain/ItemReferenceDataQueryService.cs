using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
using Common.Domain.Pagination.Interfaces;
using Common.Resources.Extensions;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain
{
    public class ItemReferenceDataQueryService : IItemReferenceDataQueryService
    {
        private readonly IItemReferenceDataRepository _itemReferenceDataRepository;

        public ItemReferenceDataQueryService(IItemReferenceDataRepository itemReferenceDataRepository)
        {
            _itemReferenceDataRepository = itemReferenceDataRepository;
        }

        public ItemReferenceDataPagedCollectionDdto GetPage(
            IPaginationDdto paginationDdto,
            Expression<Func<ItemReferenceData, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null)
        {
            IEnumerable<ItemReferenceData> results = _itemReferenceDataRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out int totalResults,
                orderBy.CreatePropertyAccessor<ItemReferenceData>(),
                orderByAscending,
                predicate,
                thenBy.CreatePropertyAccessor<ItemReferenceData>(),
                thenByAscending
            );

            return ItemReferenceDataPagedCollectionDdto.Create(
                Mapper.Map<IEnumerable<ItemReferenceData>, IList<ItemReferenceDataSummaryProjection>>(results), totalResults
            );
        }
    }
}
