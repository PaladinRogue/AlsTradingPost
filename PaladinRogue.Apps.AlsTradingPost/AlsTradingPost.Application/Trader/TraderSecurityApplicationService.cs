using System;
using AlsTradingPost.Application.Trader.Authorisation;
using AlsTradingPost.Application.Trader.Interfaces;
using AlsTradingPost.Application.Trader.Models;
using Common.Application.Authorisation;
using Common.Application.Authorisation.Policy;

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
                AuthorisationRule.Create(AuthorisationResource.Trader, AuthenticationAuthorisationAction.Register));
        }

        public TraderAdto GetById(Guid id)
        {
            return _securityApplicationService.Secure(() => _traderApplicationService.GetById(id),
                AuthorisationRule.Create(AuthorisationResource.Trader, AuthorisationAction.Get));
        }

        public TraderAdto Update(UpdateTraderAdto updateTraderAdto)
        {
            return _securityApplicationService.Secure(() => _traderApplicationService.Update(updateTraderAdto),
                AuthorisationRule.Create(AuthorisationResource.Trader, AuthorisationAction.Update),
                new TraderUpdateAuthorisationContext(updateTraderAdto.Id));
        }
    }
}
