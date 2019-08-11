using System.ComponentModel.DataAnnotations;
using PaladinRogue.Authentication.Domain.AuthenticationServices;
using PaladinRogue.Authentication.Domain.Identities.CreateClientCredential;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Domain.Identities
{
    public class ClientCredentialIdentity : AuthenticationIdentity
    {
        private const byte MaskLength = 20;
        private readonly string _emailMask = new string('*', MaskLength);

        protected ClientCredentialIdentity()
        {
        }

        private ClientCredentialIdentity(
            Identity identity,
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            CreateClientCredentialIdentityDdto createClientCredentialIdentityDdto)
        {
            Identity = identity;
            AuthenticationGrantTypeClientCredential = authenticationGrantTypeClientCredential;
            Identifier = createClientCredentialIdentityDdto.Identifier;
        }

        internal static ClientCredentialIdentity Create(
            Identity identity,
            AuthenticationGrantTypeClientCredential authenticationGrantTypeClientCredential,
            CreateClientCredentialIdentityDdto createClientCredentialIdentityDdto)
        {
            return new ClientCredentialIdentity(identity, authenticationGrantTypeClientCredential, createClientCredentialIdentityDdto);
        }

        public string Identifier
        {
            get => _emailMask;
            protected set => IdentifierHash = DataProtection.StaticHashAsync(value, DataKeys.IdentifierSalt).Result.Hash;
        }

        [Required]
        [MaxLength(FieldSizes.Protected)]
        public string IdentifierHash { get; protected set; }

        [Required]
        public virtual AuthenticationGrantTypeClientCredential AuthenticationGrantTypeClientCredential { get; protected set; }
    }
}
