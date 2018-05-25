using System;

namespace Common.Domain.Services.Domain
{
    public interface IGetByIdService<out T>
    {
        T GetById(Guid id);
    }
}
