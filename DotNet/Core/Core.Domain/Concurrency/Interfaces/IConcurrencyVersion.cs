﻿
namespace PaladinRogue.Libray.Core.Domain.Concurrency.Interfaces
{
    public interface IConcurrencyVersion
    {
        int Version { get; set; }

        string ToString();
    }
}
