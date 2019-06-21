using Common.Api.Concurrency;
using Common.Api.Links;
using Common.Api.Resources;
using Common.Api.Validation.Attributes;
using Common.Setup.Infrastructure.Constants;

namespace ApplicationManager.Api.Identities
{
    [ResourceType(ResourceTypes.Password)]
    [SelfLink(RouteDictionary.ChangePasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ChangePassword)]
    public class ChangePasswordIdentityTemplate : VersionedTemplate
    {
        [Required]
        [Length(6, 80)]
        public string Password { get; set; }

        [Required]
        [Length(6, 80)]
        public string ConfirmPassword { get; set; }
    }
}