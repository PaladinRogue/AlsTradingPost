using System;

namespace Common.ApplicationServices.Services.Query
{
    public interface ICheckExistsQueryService
    {
        bool CheckExists(Guid id);
    }
}
