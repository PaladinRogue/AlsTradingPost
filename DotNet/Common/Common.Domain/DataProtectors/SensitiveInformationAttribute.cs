using System;

namespace Common.Domain.DataProtectors
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveInformationAttribute : Attribute
    {
    }
}