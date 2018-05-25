using System;
using AlsTradingPost.Domain.TraderDomain.Exceptions;
using AlsTradingPost.Domain.TraderDomain.Models;

namespace AlsTradingPost.Domain.TraderDomain.Interfaces
{
    public interface ITraderDomainService
    {
        /// <exception cref="UserDoesNotExistDomainException"></exception>
        /// <exception cref="TraderAlreadyExistsDomainException"></exception>
        RegisteredTraderProjection Register(RegisterTraderDdto registerTraderDdto);
        TraderProjection Update(UpdateTraderDdto updateTraderDdto);
        TraderProjection GetById(Guid id);
    }
}