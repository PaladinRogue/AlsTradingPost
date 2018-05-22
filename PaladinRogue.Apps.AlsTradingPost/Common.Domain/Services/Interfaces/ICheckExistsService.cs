using System;

namespace Common.Domain.Services.Interfaces
{
    public interface ICheckExistsService
    {
        bool CheckExists(Guid id);
    }
}
