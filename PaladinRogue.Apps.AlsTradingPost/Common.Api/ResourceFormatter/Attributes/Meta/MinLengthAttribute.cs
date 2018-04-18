﻿using System;

namespace Common.Api.ResourceFormatter.Attributes.Meta
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinLengthAttribute : Attribute
    {
        public int MinLength { get; }
        
        public MinLengthAttribute(int minLength)
        {
            MinLength = minLength;
        }
    }
}