using System;
using Common.Messaging.Message.Interfaces;

namespace Common.Messaging.Message
{
    public class Message : IPreparedMessage
    {
        protected Message()
        {
        }

        protected Message(Guid id, string type, string securityToken, string payload)
        {
            Id = id;
            Type = type;
            SecurityToken = securityToken;
            Payload = payload;
        }

        public static IPreparedMessage Create(Guid id, string type, string securityToken, string payload)
        {
            return new Message(id, type, securityToken, payload);
        }

        public Guid Id { get; set; }

        public string Type { get; set; }
        
        public string SecurityToken { get; set; }
        
        public string Payload { get; set; }
    }
}