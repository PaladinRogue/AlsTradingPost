using AutoMapper;
using Common.ApplicationServices.Concurrency.Interfaces;
using Common.Domain.Concurrency.Interfaces;
using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Concurrency
{
    public class InboundConcurrencyTokenResolver : IValueResolver<IVersionedDdto, IVersionedEntity, byte[]>
    {
        private readonly IConcurrencyVersionProvider _concurrencyVersionProvider;

        public InboundConcurrencyTokenResolver(IConcurrencyVersionProvider concurrencyVersionProvider)
        {
            _concurrencyVersionProvider = concurrencyVersionProvider;
        }

        byte[] IValueResolver<IVersionedDdto, IVersionedEntity, byte[]>.Resolve(IVersionedDdto source, IVersionedEntity destination, byte[] destMember, ResolutionContext context)
        {
            return _concurrencyVersionProvider.GetConcurrencyTimeStamp(source);
        }
    }
}
