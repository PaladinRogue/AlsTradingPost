using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.PropertyTypes;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ResetPassword)]
    [SelfLink(RouteDictionary.ResetPasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ResetPassword)]
    public class ResetPasswordTemplate : ITemplate
    {
        [Required]
        public string Token { get; set; }

        [Required]
        [Length(6, 80)]
        [Password]
        public string Password { get; set; }

        [Required]
        [Length(6, 80)]
        [Password]
        public string ConfirmPassword { get; set; }
    }
}