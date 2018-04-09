using Common.Api.Authentication.Constants;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Common.Api.Filters
{
    public class AuthorizeControllerFilter : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Filters.Add(new AuthorizeFilter(Policies.AppAccess));
        }
    }
}