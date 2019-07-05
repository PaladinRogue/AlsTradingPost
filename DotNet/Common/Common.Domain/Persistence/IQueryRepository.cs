﻿using Common.Domain.Entities;

namespace Common.Domain.Persistence
{
    public interface IQueryRepository<T> : IGetByIdQuery<T>, IGetPageQuery<T>, IGetQuery<T>, IGetSingleQuery<T>, IAreAnyQuery<T> where T : IEntity
    {
    }
}
