using System;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Libray.Messaging.Setup.Infrastructure.Interfaces
{
    public interface IPreparedMessage : IMessage
    {
        Guid Id { get; }

        string SecurityToken { get; }

        string Payload { get; }
    }
}