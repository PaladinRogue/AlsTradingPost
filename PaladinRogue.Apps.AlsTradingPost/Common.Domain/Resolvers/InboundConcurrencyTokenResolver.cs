using AutoMapper;
using Common.Domain.Interfaces;
using Common.Domain.Models.Interfaces;
using Common.Domain.Providers.Interfaces;

namespace Common.Domain.Resolvers
{
    public class InboundConcurrencyTokenResolver : IValueResolver<IVersionedDdto, IEntity, byte[]>
    {
        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        public InboundConcurrencyTokenResolver(IConcurrencyVersionProvider concurrencyVersionProvider)
        {
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        byte[] IValueResolver<IVersionedDdto, IEntity, byte[]>.Resolve(IVersionedDdto source, IEntity destination, byte[] destMember, ResolutionContext context)
        {
            return _concurrencyVersionProvider.GetConcurrencyTimeStamp(source);
        }
    }
}
