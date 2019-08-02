using System;

namespace Common.Api.Authentication
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowRestrictedAppAccessAttribute : Attribute
    {
    }
}