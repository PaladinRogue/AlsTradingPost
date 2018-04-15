using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Interfaces;
using AlsTradingPost.Domain.ItemReferenceDataDomain.Models;
using AlsTradingPost.Domain.Models;
using AlsTradingPost.Domain.Persistence;
using AutoMapper;
using Common.Domain.Pagination.Interfaces;

namespace AlsTradingPost.Domain.ItemReferenceDataDomain
{
    public class ItemReferenceDataQueryService : IItemReferenceDataQueryService
    {
        private readonly IItemReferenceDataRepository _itemReferenceDataRepository;

        public ItemReferenceDataQueryService(IItemReferenceDataRepository itemReferenceDataRepository)
        {
            _itemReferenceDataRepository = itemReferenceDataRepository;
        }

        public ItemReferenceDataPagedCollectionDdto GetPage(IPaginationDdto paginationDdto, Predicate<ItemReferenceData> predicate = null)
        {
            IEnumerable<ItemReferenceData> results = _itemReferenceDataRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out int totalResults,
                predicate
            );

            return ItemReferenceDataPagedCollectionDdto.Create(
                Mapper.Map<IEnumerable<ItemReferenceData>, IList<ItemReferenceDataSummaryProjection>>(results),
                totalResults, paginationDdto
            );
        }

        public ItemReferenceDataPagedCollectionDdto GetPage<TOrderByKey>(IPaginationDdto paginationDdto, Predicate<ItemReferenceData> predicate = null,
            Expression<Func<ItemReferenceData, TOrderByKey>> orderBy = null, bool orderByAscending = true)
        {
            IEnumerable<ItemReferenceData> results = _itemReferenceDataRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out int totalResults,
                predicate,
                orderBy,
                orderByAscending
            );

            return ItemReferenceDataPagedCollectionDdto.Create(
                Mapper.Map<IEnumerable<ItemReferenceData>, IList<ItemReferenceDataSummaryProjection>>(results),
                totalResults, paginationDdto
            );
        }

        public ItemReferenceDataPagedCollectionDdto GetPage<TOrderByKey, TThenByKey>(
            IPaginationDdto paginationDdto,
            Predicate<ItemReferenceData> predicate = null,
            Expression<Func<ItemReferenceData, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<ItemReferenceData, TThenByKey>> thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<ItemReferenceData> results = _itemReferenceDataRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out int totalResults,
                predicate,
                orderBy,
                orderByAscending,
                thenBy,
                thenByAscending
            );

            return ItemReferenceDataPagedCollectionDdto.Create(
                Mapper.Map<IEnumerable<ItemReferenceData>, IList<ItemReferenceDataSummaryProjection>>(results),
                totalResults, paginationDdto
            );
        }
    }
}
