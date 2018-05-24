using System;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Services.Query;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Services
{
    public class ConcurrencyQueryService<T> : IConcurrencyQueryService<T> where T : ICheckConcurrencyService
    {
        private readonly T _queryService;

        public ConcurrencyQueryService(T queryService)
        {
            _queryService = queryService;
        }

        public void CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            if (!_queryService.CheckConcurrency(id, version))
            {
                throw new ConcurrencyDomainException(typeof(T), id, version);
            }
        }
    }
}
