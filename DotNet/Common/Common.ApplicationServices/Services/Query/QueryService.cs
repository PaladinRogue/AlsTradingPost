using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Common.Domain.Aggregates;
using Common.Domain.Pagination.Interfaces;
using Common.Domain.Persistence;
using Common.Domain.Sorting;
using Common.Resources.Sorting;

namespace Common.ApplicationServices.Services.Query
{
    public class QueryService<T> : IQueryService<T> where T : IAggregateRoot
    {
        private readonly IQueryRepository<T> _queryRepository;

        public QueryService(IQueryRepository<T> queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public IEnumerable<T> GetPage(IPaginationDdto paginationDdto,
            out int totalResults,
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

            return _queryRepository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out totalResults,
                sort,
                predicate
            );
        }

        public IEnumerable<T> Get(IList<SortBy> sort, Expression<Func<T, bool>> predicate = null)
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

            return _queryRepository.Get(
                sort,
                predicate
            );
        }
    }
}
