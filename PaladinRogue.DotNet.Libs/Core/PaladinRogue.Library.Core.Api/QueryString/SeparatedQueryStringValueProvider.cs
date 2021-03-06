﻿using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace PaladinRogue.Library.Core.Api.QueryString
{
    public class SeparatedQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string _key;
        private readonly string _separator;

        public SeparatedQueryStringValueProvider(IQueryCollection values, string separator)
            : this(null, values, separator)
        {
        }

        public SeparatedQueryStringValueProvider(string key, IQueryCollection values, string separator)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _separator = separator;
        }

        public override ValueProviderResult GetValue(string key)
        {
            ValueProviderResult result = base.GetValue(key);

            if (_key != null && _key != key)
            {
                return result;
            }

            if (result != ValueProviderResult.None &&
                result.Values.Any(x => x.IndexOf(_separator, StringComparison.OrdinalIgnoreCase) > 0))
            {
                StringValues splitValues = new StringValues(
                    result.Values.SelectMany(x => x.Split(new[] { _separator },
                        StringSplitOptions.None)).ToArray()
                );
                return new ValueProviderResult(splitValues, result.Culture);
            }

            return result;
        }
    }
}
