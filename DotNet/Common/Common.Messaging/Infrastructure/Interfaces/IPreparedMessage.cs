using System;

namespace Common.Messaging.Infrastructure.Interfaces
{
    public interface IPreparedMessage : IMessage
    {
        Guid Id { get; }
        
        string SecurityToken { get; }

        string Payload { get; }
    }
}