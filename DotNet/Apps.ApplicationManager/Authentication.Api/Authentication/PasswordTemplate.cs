using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.PropertyTypes;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Api.Validation.Attributes;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Authentication
{
    [ResourceType(ResourceTypes.AuthenticatePassword)]
    [SelfLink(RouteDictionary.AuthenticatePasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.AuthenticatePassword)]
    public class PasswordTemplate : ITemplate
    {
        [Required]
        [MaxLength(FieldSizes.Default)]
        public string Identifier { get; set; }

        [Required]
        [MaxLength(FieldSizes.Default)]
        [Password]
        public string Password { get; set; }
    }
}