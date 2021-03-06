﻿using System;
using PaladinRogue.Library.Core.Domain.Validation;

namespace PaladinRogue.Library.Core.Application.Exceptions
{
    public class BusinessValidationRuleApplicationException : Exception
    {
        public ValidationResult ValidationResult { get; }

        public BusinessValidationRuleApplicationException(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
