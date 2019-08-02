using System;
using Common.Messaging.Infrastructure.Messages;

namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IPreparedMessage : IMessage
    {
        Guid Id { get; }
        
        string SecurityToken { get; }

        string Payload { get; }
    }
}