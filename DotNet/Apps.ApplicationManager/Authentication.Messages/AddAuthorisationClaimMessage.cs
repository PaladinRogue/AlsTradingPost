using System;
using PaladinRogue.Libray.Messaging.Common.Messages;

namespace PaladinRogue.Authentication.Messages
{
    public class AddAuthorisationClaimMessage : IMessage
    {
        protected AddAuthorisationClaimMessage()
        {
        }

        protected AddAuthorisationClaimMessage(Guid identityId, string claimType, string value)
        {
            IdentityId = identityId;
            ClaimType = claimType;
            Value = value;
        }

        public static AddAuthorisationClaimMessage Create(Guid identityId, string claimType, string value)
        {
            return new AddAuthorisationClaimMessage(identityId, claimType, value);
        }

        public string Type => nameof(AddAuthorisationClaimMessage);

        public Guid IdentityId { get; set; }

        public string ClaimType { get; set; }

        public string Value { get; set; }
    }
}
