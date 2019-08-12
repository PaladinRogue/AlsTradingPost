using Microsoft.AspNetCore.Http;
using PaladinRogue.Library.Core.Application.Concurrency;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;
using PaladinRogue.Library.Core.Setup.Infrastructure.Exceptions;

namespace PaladinRogue.Library.Core.Setup.Infrastructure.Concurrency
{
    public class ConcurrencyVersionProvider : IConcurrencyVersionProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConcurrencyVersionProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IConcurrencyVersion Get()
        {
            string concurrencyValue = _httpContextAccessor.HttpContext.Request.Headers[ConcurrencyHeaders.IfMatch];

            if (concurrencyValue == null) throw new PreConditionFailedException();

            return ConcurrencyVersionFactory.CreateFromBase64String(concurrencyValue);
        }
    }
}