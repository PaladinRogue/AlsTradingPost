﻿
namespace Common.Api.Interfaces
{
    public interface IVersionedResource : IVersioned<IConcurrencyVersion>
    {
    }
}
