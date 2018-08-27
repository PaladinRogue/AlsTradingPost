using System.Collections.Generic;
using Authentication.Application.Application.Models;
using Common.Resources.Authentication;

namespace Authentication.Application.Application.Interfaces
{
    public interface IApplicationApplicationKernalService
    {
        IEnumerable<ApplicationAdto> Get(AuthenticationProtocol authenticationProtocol);
    }
}

