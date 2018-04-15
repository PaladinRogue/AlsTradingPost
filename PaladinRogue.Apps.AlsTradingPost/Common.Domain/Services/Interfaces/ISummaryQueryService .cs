﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Domain.Services.Interfaces
{
    public interface ISummaryQueryService<T, TOut>
    {
        IList<TOut> Get(Predicate<T> predicate = null);

        IList<TOut> Get<TOrderByKey>(Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true);

        IList<TOut> Get<TOrderByKey, TThenByKey>(Predicate<T> predicate = null,
            Expression<Func<T, TOrderByKey>> orderBy = null,
            bool orderByAscending = true,
            Expression<Func<T, TThenByKey>> thenBy = null,
            bool thenByAscending = true);
    }
}
