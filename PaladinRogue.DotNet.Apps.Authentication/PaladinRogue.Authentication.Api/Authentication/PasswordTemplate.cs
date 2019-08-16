using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.PropertyTypes;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

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