﻿using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Domain.Concurrency.Interfaces;

namespace PaladinRogue.Library.Core.Api.Concurrency.Interfaces
{
    public interface IVersionedTemplate : IVersioned<IConcurrencyVersion>, ITemplate
    {
    }
}
