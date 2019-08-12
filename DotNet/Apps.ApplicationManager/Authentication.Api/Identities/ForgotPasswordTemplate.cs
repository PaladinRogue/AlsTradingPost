using PaladinRogue.Library.Core.Api.Links;
using PaladinRogue.Library.Core.Api.PropertyTypes;
using PaladinRogue.Library.Core.Api.Resources;
using PaladinRogue.Library.Core.Api.Validation.Attributes;
using PaladinRogue.Library.Core.Common;
using PaladinRogue.Library.Core.Setup.Infrastructure.Constants;

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