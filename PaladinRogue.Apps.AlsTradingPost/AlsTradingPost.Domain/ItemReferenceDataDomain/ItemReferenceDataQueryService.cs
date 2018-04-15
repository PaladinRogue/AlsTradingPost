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

        public ItemReferenceDataPagedCollectionDdto GetPage(
            IPaginationDdto paginationDdto,
            Predicate<ItemReferenceData> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool thenByAscending = true)
        {
            IEnumerable<ItemReferenceData> results = _itemReferenceDataRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out int totalResults,
                predicate,
                CreatePropertyAccessor<ItemReferenceData>(orderBy),
                orderByAscending,
                CreatePropertyAccessor<ItemReferenceData>(thenBy),
                thenByAscending
            );

            return ItemReferenceDataPagedCollectionDdto.Create(
                Mapper.Map<IEnumerable<ItemReferenceData>, IList<ItemReferenceDataSummaryProjection>>(results),
                totalResults, paginationDdto
            );
        }

        private static Func<TIn, object> CreatePropertyAccessor<TIn>(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return null;
            }

            ParameterExpression param = Expression.Parameter(typeof(TIn));
            MemberExpression body = Expression.PropertyOrField(param, propertyName);
            return Expression.Lambda<Func<TIn, object>>(body, param).Compile();
        }
    }
}
