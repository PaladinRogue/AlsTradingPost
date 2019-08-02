using Common.Api.Links;
using Common.Api.Resources;
using Common.Setup.Infrastructure.Constants;

namespace Authentication.Api.Identities
{
    [ResourceType(ResourceTypes.ForgotPassword)]
    [Link(LinkDictionary.ResetPassword, RouteDictionary.ResetPasswordResourceTemplate, HttpVerb.Get)]
    public class ForgotPasswordResource : IResource
    {
    }
}