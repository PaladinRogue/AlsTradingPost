using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;
using Common.Domain.Pagination.Interfaces;
using Common.Domain.Persistence;
using Common.Resources.Concurrency.Interfaces;
using Common.Resources.Extensions;

namespace Common.Domain.Services.Query
{
    public class QueryService<T> : IQueryService<T> where T : IVersionedEntity
    {
        private readonly IRepository<T> _repository;

        public QueryService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T GetById(Guid id)
        {
            return _repository.GetById(id);
        }
        
        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetSingle(predicate);
        }

        public IEnumerable<T> GetPage(IPaginationDdto paginationDdto,
            out int totalResults,
            Expression<Func<T, bool>> predicate = null,
            string orderBy = null,
            bool orderByAscending = true,
            string thenBy = null,
            bool? thenByAscending = null)
        {
            return _repository.GetPage(
                paginationDdto.PageSize,
                paginationDdto.PageOffset,
                out totalResults,
                orderBy.CreatePropertyAccessor<T>(),
                orderByAscending,
                predicate,
                thenBy.CreatePropertyAccessor<T>(),
                thenByAscending
            );
        }

        public bool CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            return _repository.CheckConcurrency(id, version);
        }

        public bool CheckExists(Guid id)
        {
            return _repository.CheckExists(id);
        }
    }
}
