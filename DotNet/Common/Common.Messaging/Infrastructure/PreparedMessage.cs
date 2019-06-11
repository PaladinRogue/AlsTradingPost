using System;
using Common.Messaging.Infrastructure.Interfaces;

namespace Common.Messaging.Infrastructure
{
    public class PreparedMessage : IPreparedMessage
    {
        protected PreparedMessage()
        {
        }

        protected PreparedMessage(Guid id, string type, string securityToken, string payload)
        {
            Id = id;
            Type = type;
            SecurityToken = securityToken;
            Payload = payload;
        }

        public static IPreparedMessage Create(Guid id, string type, string securityToken, string payload)
        {
            return new PreparedMessage(id, type, securityToken, payload);
        }

        public Guid Id { get; set; }

        public string Type { get; set; }
        
        public string SecurityToken { get; set; }
        
        public string Payload { get; set; }
    }
}