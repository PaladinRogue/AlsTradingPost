using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using PaladinRogue.Libray.Core.Api.Authentication;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Authorisation;

namespace PaladinRogue.Libray.Core.Api.Authorisation
{
    public class AuthoriseAppAccessControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            foreach (ActionModel controllerAction in controller.Actions)
            {
                if (controllerAction.Attributes.Any(a => a is AllowRestrictedAppAccessAttribute))
                {
                    controllerAction.Filters.Add(new AuthorizeFilter(Policies.RestrictedAppAccess));
                }
                else
                {
                    controllerAction.Filters.Add(new AuthorizeFilter(Policies.AppAccess));
                }
            }
        }
    }
}