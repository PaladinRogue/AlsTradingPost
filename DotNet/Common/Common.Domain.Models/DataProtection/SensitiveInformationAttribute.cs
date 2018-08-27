using System;

namespace Common.Domain.Models.DataProtection
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveInformationAttribute : Attribute
    {
    }
}