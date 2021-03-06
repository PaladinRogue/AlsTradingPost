﻿using System;

namespace PaladinRogue.Library.Core.Api.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute
    {
        public int MaxLength { get; }

        public MaxLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }
    }
}