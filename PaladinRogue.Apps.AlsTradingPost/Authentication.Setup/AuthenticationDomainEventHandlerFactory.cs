using System.Collections.Generic;
using Common.Domain.DomainEvents.Interfaces;

namespace Authentication.Setup
{
    public class AuthenticationDomainEventHandlerFactory
    {
	    public AuthenticationDomainEventHandlerFactory(IEnumerable<IDomainEventHandler> handlers)
	    {
		    foreach (IDomainEventHandler domainEventHandler in handlers)
		    {
			    domainEventHandler.Register();
		    }
	    }
    }
}
