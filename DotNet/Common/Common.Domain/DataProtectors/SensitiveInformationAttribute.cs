﻿using System;

namespace Common.Domain.DataProtectors
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SensitiveInformationAttribute : Attribute
    {
        public SensitiveInformationAttribute(string keyName)
        {
            KeyName = keyName;
        }

        public string KeyName { get; }
    }
}