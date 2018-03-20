using System;
using System.Linq;
using Common.Domain.ConcurrencyServices.Interfaces;
using Common.Domain.Exceptions;
using Common.Domain.Interfaces;
using Common.Resources.Concurrency;

namespace Common.Domain.ConcurrencyServices
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
            var entity = _queryService.Get(id);
            if (!entity.Version.Version.SequenceEqual(version.Version))
            {
                throw new ConcurrencyDomainException(typeof(T), id, version);
            }
        }
    }
}
