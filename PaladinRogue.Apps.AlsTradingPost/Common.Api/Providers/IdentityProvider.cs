using System;
using Common.Api.Providers.Interfaces;

namespace Common.Api.Providers
{
    public class IdentityProvider : IIdentityProvider
    {
        public Guid Id { get; set; }
    }
}
