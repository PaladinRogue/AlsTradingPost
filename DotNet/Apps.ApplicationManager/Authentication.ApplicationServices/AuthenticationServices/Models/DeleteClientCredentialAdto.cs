using System;
using Common.ApplicationServices.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace Authentication.ApplicationServices.AuthenticationServices.Models
{
    public class DeleteClientCredentialAdto : IInboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}