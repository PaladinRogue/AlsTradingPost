using System;
using Common.Domain.Models.Interfaces;

namespace Common.ApplicationServices.Services.Query
{
    public interface IGetByIdQueryService<out T> where T : IEntity
    {
        T GetById(Guid id);
    }
}
