using Common.Application.Concurrency;
using Common.Domain.Concurrency.Interfaces;
using Common.Setup.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Common.Setup.Infrastructure.Concurrency
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