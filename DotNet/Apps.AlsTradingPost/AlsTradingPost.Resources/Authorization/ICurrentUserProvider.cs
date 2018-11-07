﻿using System;
using System.Collections.Generic;

namespace AlsTradingPost.Resources.Authorization
{
    public interface ICurrentUserProvider
    {
        Guid Id { get; }

        IReadOnlyDictionary<Type, Guid> WhoAmI { get; }
    }
}