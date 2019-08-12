using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.PropertyTypes;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    [SelfLink(RouteDictionary.RegisterPasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.RegisterPassword)]
    public class RegisterPasswordIdentityTemplate : ITemplate
    {
        [Required]
        [EmailAddress]
        [MaxLength(FieldSizes.Extended)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(FieldSizes.Extended)]
        public string Identifier { get; set; }

        [Required]
        [Length(6, FieldSizes.Default)]
        [Password]
        public string Password { get; set; }

        [Required]
        [Length(6, FieldSizes.Default)]
        [Password]
        public string ConfirmPassword { get; set; }
    }
}