using System;
using PaladinRogue.Library.Authorisation.Common.Rules;

namespace PaladinRogue.Library.Authorisation.Common.Contexts
{
    public interface IAuthorisationContext : IAuthorisationRule
    {
        Type ResourceType { get; }

        Guid? ResourceId { get; }
    }
}