using System;
using Common.Resources.Concurrency.Interfaces;

namespace Common.ApplicationServices.Concurrency.Services.Interfaces
{
    public interface ICheckConcurrencyService
    {
        bool CheckConcurrency(Guid id, IConcurrencyVersion version);
    }
}
