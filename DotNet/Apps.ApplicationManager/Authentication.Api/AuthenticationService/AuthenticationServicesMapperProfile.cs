using Authentication.Api.AuthenticationService.Facebook;
using Authentication.Api.AuthenticationService.Google;
using Authentication.ApplicationServices.AuthenticationServices.Models;
using Authentication.ApplicationServices.AuthenticationServices.Models.Facebook;
using Authentication.ApplicationServices.AuthenticationServices.Models.Google;
using AutoMapper;

namespace Authentication.Api.AuthenticationService
{
    public class AuthenticationServicesMapperProfile : Profile
    {
        public AuthenticationServicesMapperProfile()
        {
            CreateMap<AuthenticationServiceTypeAdto, AuthenticationServiceTypeResource>()
                .Include<FacebookAuthenticationServiceTypeAdto, FacebookAuthenticationServiceTypeResource>()
                .Include<GoogleAuthenticationServiceTypeAdto, GoogleAuthenticationServiceTypeResource>();

            CreateMap<FacebookAuthenticationServiceTypeAdto, FacebookAuthenticationServiceTypeResource>();
            CreateMap<GoogleAuthenticationServiceTypeAdto, GoogleAuthenticationServiceTypeResource>();

            CreateMap<AuthenticationServiceAdto, AuthenticationServiceSummaryResource>()
                .Include<PasswordAuthenticationServiceAdto, PasswordAuthenticationServiceSummaryResource>()
                .Include<RefreshTokenAuthenticationServiceAdto, RefreshTokenAuthenticationServiceSummaryResource>()
                .Include<FacebookAuthenticationServiceAdto, FacebookAuthenticationServiceSummaryResource>()
                .Include<GoogleAuthenticationServiceAdto, GoogleAuthenticationServiceSummaryResource>();

            CreateMap<PasswordAuthenticationServiceAdto, PasswordAuthenticationServiceSummaryResource>();
            CreateMap<RefreshTokenAuthenticationServiceAdto, RefreshTokenAuthenticationServiceSummaryResource>();
            CreateMap<FacebookAuthenticationServiceAdto, FacebookAuthenticationServiceSummaryResource>();
            CreateMap<GoogleAuthenticationServiceAdto, GoogleAuthenticationServiceSummaryResource>();
        }
    }
}