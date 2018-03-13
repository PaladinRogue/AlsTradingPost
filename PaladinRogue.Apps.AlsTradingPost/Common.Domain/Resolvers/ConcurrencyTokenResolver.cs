using AutoMapper;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Resolvers
{
    public class ConcurrencyTokenResolver : IValueResolver<IEntity, object, int>
    {
        private readonly IConcurrencyTokenProvider _concurrencyTokenProvider;

        public ConcurrencyTokenResolver(IConcurrencyTokenProvider concurrencyTokenProvider)
        {
            _concurrencyTokenProvider = concurrencyTokenProvider;
        }

        int IValueResolver<IEntity, object, int>.Resolve(IEntity source, object destination, int destMember, ResolutionContext context)
        {
            return _concurrencyTokenProvider.GetVersion(source);
        }
    }
}
