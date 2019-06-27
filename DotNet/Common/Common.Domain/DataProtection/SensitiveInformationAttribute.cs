using System;

namespace Common.Domain.DataProtection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveInformationAttribute : Attribute
    {
    }
}