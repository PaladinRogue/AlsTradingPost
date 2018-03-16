using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Resolvers
{
    public class OutboundConcurrencyTokenResolver : IValueResolver<IEntity, IVersionedProjection, int>
    {
        private readonly IConcurrencyTokenProvider _concurrencyTokenProvider;

        public OutboundConcurrencyTokenResolver(IConcurrencyTokenProvider concurrencyTokenProvider)
        {
            _concurrencyTokenProvider = concurrencyTokenProvider;
        }

        int IValueResolver<IEntity, IVersionedProjection, int>.Resolve(IEntity source, IVersionedProjection destination, int destMember, ResolutionContext context)
        {
            return _concurrencyTokenProvider.GetConcurrencyToken(source);
        }
    }
}
