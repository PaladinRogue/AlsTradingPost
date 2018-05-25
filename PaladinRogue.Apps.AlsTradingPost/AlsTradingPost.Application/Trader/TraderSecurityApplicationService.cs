using System;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using Common.Application.Authorisation;

namespace AlsTradingPost.Application.Trader
{
    public class TraderSecurityApplicationService : ISecure<ITraderApplicationService>, ITraderApplicationService
    {
        private readonly ITraderApplicationService _traderApplicationService;
        private readonly ISecurityApplicationService _securityApplicationService;

        public ITraderApplicationService Service => this;

        public TraderSecurityApplicationService(
            ITraderApplicationService traderApplicationService,
            ISecurityApplicationService securityApplicationService)
        {
            _traderApplicationService = traderApplicationService;
            _securityApplicationService = securityApplicationService;
        }

        public RegisteredTraderAdto Register(RegisterTraderAdto registerTraderAdto)
        {
            return _securityApplicationService.Secure(() => _traderApplicationService.Register(registerTraderAdto),
                new AuthorisationRule());
        }

        public TraderAdto GetById(Guid id)
        {
            return _securityApplicationService.Secure(() => _traderApplicationService.GetById(id),
                new AuthorisationRule());
        }

        public TraderAdto Update(UpdateTraderAdto updateTraderAdto)
        {
            return _securityApplicationService.Secure(() => _traderApplicationService.Update(updateTraderAdto),
                new AuthorisationRule());
        }
    }
}
