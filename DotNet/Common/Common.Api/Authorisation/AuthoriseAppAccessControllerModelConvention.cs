﻿using System.Linq;
using Common.Api.Authorisation;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Common.Api.Authentication
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