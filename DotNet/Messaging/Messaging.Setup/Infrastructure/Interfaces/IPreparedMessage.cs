using System;
using Messaging.Common;

namespace Messaging.Setup.Infrastructure.Interfaces
{
    public interface IPreparedMessage : IMessage
    {
        Guid Id { get; }
        
        string SecurityToken { get; }

        string Payload { get; }
    }
}