using System;
using System.Linq.Expressions;
using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Services.Query
{
    public interface IGetSingleQueryService<T> where T : IEntity
    {
        T GetSingle(Expression<Func<T, bool>> predicate);
    }
}
