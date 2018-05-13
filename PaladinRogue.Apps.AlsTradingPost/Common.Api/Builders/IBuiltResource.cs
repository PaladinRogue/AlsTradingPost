﻿using System.Collections.Generic;

namespace Common.Api.Builders
{
    public interface IBuiltResource
    {
        object Data { get; set; }

        IDictionary<string, Dictionary<string, object>> Meta { get; set; }
        
        IDictionary<string, IDictionary<string, object>> Links { get; set; }
    }
}