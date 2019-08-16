using System;

namespace PaladinRogue.Library.Core.Api.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowRestrictedAppAccessAttribute : Attribute
    {
    }
}