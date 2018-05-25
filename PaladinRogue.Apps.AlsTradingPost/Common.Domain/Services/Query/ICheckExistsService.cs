using System;

namespace Common.Domain.Services.Query
{
    public interface ICheckExistsService
    {
        bool CheckExists(Guid id);
    }
}
