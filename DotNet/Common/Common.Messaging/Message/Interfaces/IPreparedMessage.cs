using System;

namespace Common.Messaging.Message.Interfaces
{
    public interface IPreparedMessage : IMessage
    {
        Guid Id { get; }
        
        string SecurityToken { get; }

        string Payload { get; }
    }
}