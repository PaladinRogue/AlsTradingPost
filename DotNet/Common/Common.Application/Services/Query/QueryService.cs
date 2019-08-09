using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Domain.Aggregates;
using Common.Domain.Models;
using Common.Domain.Pagination.Interfaces;
using Common.Domain.Persistence;
using Common.Domain.Sorting;
using Common.Resources.Sorting;

namespace Common.Application.Services.Query
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
