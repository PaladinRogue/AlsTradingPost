using System;
using PaladinRogue.Library.Messaging.Common.Messages;

namespace PaladinRogue.Library.Messaging.Setup.Infrastructure.Interfaces
{
    public interface IPreparedMessage : IMessage
    {
        Guid Id { get; }

        string SecurityToken { get; }

        string Payload { get; }
    }
}