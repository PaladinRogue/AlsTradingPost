﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Common.Api.Exceptions;

namespace Common.Api.Formatters
{
    public class JsonV1ApplicationErrorFormatter : IApplicationErrorFormatter<FormattedError>
    {
        public FormattedError Format(ApplicationError applicationError)
        {
            return Format(new List<ApplicationError>
            {
                applicationError
            });
        }

        public FormattedError Format(IEnumerable<ApplicationError> applicationErrors)
        {
            return new FormattedError
            {
                Errors = applicationErrors.Select(e => new Error
                {
                    Status = (int)e.HttpStatusCode,
                    Title = Enum.GetName(typeof(HttpStatusCode), e.HttpStatusCode),
                    Detail = e.Exception.Message
                })
            };
        }
    }
}
