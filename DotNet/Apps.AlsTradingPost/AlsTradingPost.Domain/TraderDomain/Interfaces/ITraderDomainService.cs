using System;
using AlsTradingPost.Domain.TraderDomain.Exceptions;
using AlsTradingPost.Domain.TraderDomain.Models;
using Common.Domain.Services.Domain;

namespace AlsTradingPost.Domain.TraderDomain.Interfaces
{
    public interface ITraderDomainService : ICheckConcurrencyService
    {
        /// <exception cref="UserDoesNotExistDomainException"></exception>
        /// <exception cref="TraderAlreadyExistsDomainException"></exception>
        RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto);
        TraderProjection Update(UpdateTraderDdto updateTraderDdto);
        TraderProjection GetById(Guid id);
    }
}