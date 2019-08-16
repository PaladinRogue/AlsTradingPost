using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PaladinRogue.Library.Core.Common.Sorting;
using PaladinRogue.Library.Core.Domain.Aggregates;
using PaladinRogue.Library.Core.Domain.Models;
using PaladinRogue.Library.Core.Domain.Persistence;

namespace PaladinRogue.Library.Core.Application.Caching
{
    public class QueryRepositoryCacheDecorator<T> : CacheDecorator<Guid, EntityCacheKey<T>, T>, IQueryRepository<T> where T : class, IAggregateRoot
    {
        private readonly IQueryRepository<T> _queryRepository;

        private readonly ICacheService _cacheService;

        public QueryRepositoryCacheDecorator(
            IQueryRepository<T> queryRepository,
            ICacheService cacheService) : base(cacheService)
        {
            _queryRepository = queryRepository;
            _cacheService = cacheService;
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _cacheService.GetOrAddAsync(new EntityCacheKey<T>(id), () => _queryRepository.GetByIdAsync(id));
        }

        public Task<IQueryable<T>> GetAsync(IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null)
        {
            return _queryRepository.GetAsync(sort, predicate);
        }

        public Task<IPagedResult<T>> GetPageAsync(int pageSize,
            int pageOffset,
            IList<SortBy> sort,
            Expression<Func<T, bool>> predicate = null)
        {
            return _queryRepository.GetPageAsync(pageSize, pageOffset, sort, predicate);
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryRepository.GetSingleAsync(predicate);
        }

        public Task<bool> AreAnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _queryRepository.AreAnyAsync(predicate);
        }

        protected override EntityCacheKey<T> CreateCacheKey(Guid key)
        {
            return new EntityCacheKey<T>(key);
        }
    }
}