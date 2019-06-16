using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.PasswordIdentity)]
    [SelfLink(RouteDictionary.PasswordIdentityResourceTemplate, HttpVerb.Get)]
    public class CreatePasswordIdentityTemplate : VersionedTemplate
    {
        [Required]
        public string Token { get; set; }

        public string Identifier { get; set; }

        [Required]
        [Length(6, 80)]
        public string Password { get; set; }

        [Required]
        [Length(6, 80)]
        public string ConfirmPassword { get; set; }
    }
}