using AutoMapper;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Resources.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency
{
    public class OutboundConcurrencyTokenResolver : IValueResolver<IVersionedEntity, IVersionedProjection, IConcurrencyVersion>
    {
        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        public OutboundConcurrencyTokenResolver(IConcurrencyVersionProvider concurrencyVersionProvider)
        {
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        IConcurrencyVersion IValueResolver<IVersionedEntity, IVersionedProjection, IConcurrencyVersion>.Resolve(IVersionedEntity source, IVersionedProjection destination, IConcurrencyVersion destMember, ResolutionContext context)
        {
            return _concurrencyVersionProvider.GetConcurrencyVersion(source);
        }
    }
}
