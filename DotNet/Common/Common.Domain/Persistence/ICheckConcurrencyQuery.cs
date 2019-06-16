﻿using System;
using Common.Domain.Concurrency.Interfaces;

namespace Common.Domain.Persistence
{
    public interface ICheckConcurrencyQuery<T>
    {
        /// <summary>Returns whether an entity exists with the given id and version.</summary>
        /// <param name="id">The id of the entity to check.</param>
        /// <param name="version">The version of the entity to check.</param>
        /// <returns>The entity which exists with id and version.</returns>
        T GetWithConcurrencyCheck(Guid id, IConcurrencyVersion version);
    }
}