using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Resolvers
{
    public class OutboundConcurrencyTokenResolver : IValueResolver<IEntity, IVersionedProjection, IConcurrencyVersion>
    {
        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        public OutboundConcurrencyTokenResolver(IConcurrencyVersionProvider concurrencyVersionProvider)
        {
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        IConcurrencyVersion IValueResolver<IEntity, IVersionedProjection, IConcurrencyVersion>.Resolve(IEntity source, IVersionedProjection destination, IConcurrencyVersion destMember, ResolutionContext context)
        {
            return _concurrencyVersionProvider.GetConcurrencyVersion(source);
        }
    }
}
