using System;

namespace PaladinRogue.Libray.Core.Api.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowRestrictedAppAccessAttribute : Attribute
    {
    }
}