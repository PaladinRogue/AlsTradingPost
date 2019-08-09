using System;
using Common.Application.Concurrency;
using Common.Domain.Concurrency.Interfaces;

namespace Authentication.Application.AuthenticationServices.Models
{
    public class DeleteClientCredentialAdto : IInboundVersionedAdto
    {
        public Guid Id { get; set; }

        public IConcurrencyVersion Version { get; set; }
    }
}