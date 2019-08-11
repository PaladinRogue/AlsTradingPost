using System.ComponentModel.DataAnnotations;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Domain.DataProtectors;

namespace PaladinRogue.Authentication.Domain.AuthenticationServices
{
    public class AuthenticationGrantTypeClientCredential : AuthenticationService
    {
        private const byte MaskLength = 8;
        private readonly string _clientMask = new string('*', MaskLength);

        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Name { get; protected set; }

        [SensitiveInformation(DataKeys.ClientId)]
        [MaxLength(FieldSizes.Default)]
        [Required]
        public string ClientId { get; protected set; }

        [Required]
        [SensitiveInformation(DataKeys.ClientSecret)]
        [MaxLength(FieldSizes.Default)]
        public string ClientSecret { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string ClientGrantAccessTokenUrl { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string GrantAccessTokenUrl { get; protected set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string ValidateAccessTokenUrl { get; protected set; }

        public string MaskedClientId => _clientMask;

        public string MaskedClientSecret => _clientMask;
    }
}
