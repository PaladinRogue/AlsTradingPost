﻿using System.Collections.Generic;
using PaladinRogue.Library.Core.Api.Exceptions;

namespace PaladinRogue.Library.Core.Api.Formats.JsonV1.Formats
{
    public class FormattedError : IFormattedError
    {
        protected FormattedError()
        {
        }

        public IEnumerable<Error> Errors { get; set; }

        public static FormattedError Create(Error error)
        {
            return Create(new List<Error>{ error });
        }

        public static FormattedError Create(IEnumerable<Error> errors)
        {
            return new FormattedError
            {
                Errors = errors
            };
        }
    }
}
