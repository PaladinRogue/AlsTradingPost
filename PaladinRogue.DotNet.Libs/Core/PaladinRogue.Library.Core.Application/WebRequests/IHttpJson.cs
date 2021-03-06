﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PaladinRogue.Library.Core.Application.WebRequests
{
    public interface IHttpJson
    {
	    Task<T> GetAsync<T>(Uri requestUri, JsonSerializerSettings jsonSerializerSettings);

	    Task<T> PostAsync<T>(Uri requestUri, JsonSerializerSettings jsonSerializerSettings);

	    Task<TOut> PostAsync<TIn, TOut>(Uri requestUri, TIn request, JsonSerializerSettings jsonSerializerSettings);
    }
}
