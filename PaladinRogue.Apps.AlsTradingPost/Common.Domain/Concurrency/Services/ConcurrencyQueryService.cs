using System;
using System.Linq;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Concurrency.Services.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Services;
using Common.Domain.Services.Interfaces;
using Common.Resources.Concurrency;
using Common.Resources.Concurrency.Interfaces;

namespace Common.Domain.Concurrency.Services
{
    public class ConcurrencyQueryService<T> : IConcurrencyQueryService<T> where T : IQueryService<IVersionedProjection>
    {
        private readonly T _queryService;

        public ConcurrencyQueryService(T queryService)
        {
            _queryService = queryService;
        }

        public void CheckConcurrency(Guid id, IConcurrencyVersion version)
        {
            IVersionedProjection entity = _queryService.GetById(id);
            if (!entity.Version.Version.SequenceEqual(version.Version))
            {
                throw new ConcurrencyDomainException(typeof(T), id, version);
            }
        }
    }
}
