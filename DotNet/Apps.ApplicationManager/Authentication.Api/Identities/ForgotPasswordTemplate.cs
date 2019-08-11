using PaladinRogue.Libray.Core.Api.Links;
using PaladinRogue.Libray.Core.Api.PropertyTypes;
using PaladinRogue.Libray.Core.Api.Resources;
using PaladinRogue.Libray.Core.Api.Validation.Attributes;
using PaladinRogue.Libray.Core.Common;
using PaladinRogue.Libray.Core.Setup.Infrastructure.Constants;

namespace PaladinRogue.Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ForgotPassword)]
    [SelfLink(RouteDictionary.ForgotPasswordResourceTemplate, HttpVerb.Get)]
    [CreateLink(RouteDictionary.ForgotPassword)]
    public class ForgotPasswordTemplate : ITemplate
    {
        [Required]
        [EmailAddress]
        [MaxLength(FieldSizes.Extended)]
        public string EmailAddress { get; set; }
    }
}