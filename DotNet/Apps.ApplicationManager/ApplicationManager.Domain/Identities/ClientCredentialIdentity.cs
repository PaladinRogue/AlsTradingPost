using System.ComponentModel.DataAnnotations;
using ApplicationManager.Domain.AuthenticationServices;
using ApplicationManager.Domain.Identities.CreateClientCredential;
using Common.Domain.DataProtectors;
using Common.Resources;

namespace ApplicationManager.Domain.Identities
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
