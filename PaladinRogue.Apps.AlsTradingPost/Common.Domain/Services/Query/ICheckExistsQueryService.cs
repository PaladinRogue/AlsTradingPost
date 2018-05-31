using System;

namespace Common.Domain.Services.Query
{
    public interface ICheckExistsQueryService
    {
        bool CheckExists(Guid id);
    }
}
