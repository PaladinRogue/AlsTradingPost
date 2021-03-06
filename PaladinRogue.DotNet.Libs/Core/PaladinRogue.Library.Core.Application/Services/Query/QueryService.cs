﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Common.Sorting;
using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.Models;
using PaladinRogue.Library.Core.Domain.Pagination.Interfaces;
using PaladinRogue.Library.Core.Domain.Persistence;
using PaladinRogue.Library.Core.Domain.Sorting;

namespace PaladinRogue.Library.Core.Application.Services.Query
{
    public class QueryService<T> : IQueryService<T> where T : IAggregateRoot
    {
        private readonly IQueryRepository<T> _queryRepository;

        public QueryService(IQueryRepository<T> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public Task<IPagedResult<T>> GetPageAsync(
            IPaginationDdto paginationDdto,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null)
        {
            if (sort != null)
            {
                IEnumerable<string> sortableProperties = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(SortableAttribute))).Select(p => p.Name.ToLowerInvariant()).ToList();
                foreach (SortBy sortBy in sort)
                {
                    if (!sortableProperties.Contains(sortBy.PropertyName.ToLowerInvariant()))
                    {
                        throw new PropertyNotSortableException(sortBy.PropertyName);
                    }
                }
            }

            return _queryRepository.GetPageAsync(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                sort,
                predicate
            );
        }

        public Task<IQueryable<T>> GetAsync(
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null)
        {
            if (sort != null)
            {
                IEnumerable<string> sortableProperties = typeof(T).GetProperties().Where(p => Attribute.IsDefined(p, typeof(SortableAttribute))).Select(p => p.Name.ToLowerInvariant()).ToList();
                foreach (SortBy sortBy in sort)
                {
                    if (!sortableProperties.Contains(sortBy.PropertyName.ToLowerInvariant()))
                    {
                        throw new PropertyNotSortableException(sortBy.PropertyName);
                    }
                }
            }

            return _queryRepository.GetAsync(
                sort,
                predicate
            );
        }
    }
}
