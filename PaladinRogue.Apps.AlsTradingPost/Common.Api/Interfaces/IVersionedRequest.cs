﻿
namespace Common.Api.Interfaces
{
    public interface IVersionedRequest : IVersioned<IConcurrencyVersion>
    {
    }
}
