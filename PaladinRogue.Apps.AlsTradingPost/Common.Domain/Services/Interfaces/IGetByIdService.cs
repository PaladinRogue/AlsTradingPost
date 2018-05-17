using System;

namespace Common.Domain.Services.Interfaces
{
    public interface IGetByIdService<out T>
    {
        T GetById(Guid id);
    }
}
