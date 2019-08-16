using AutoMapper;
using PaladinRogue.Authentication.Api.AuthenticationService.Facebook;
using PaladinRogue.Authentication.Api.AuthenticationService.Google;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Facebook;
using PaladinRogue.Authentication.Application.AuthenticationServices.Models.Google;

namespace PaladinRogue.Authentication.Api.AuthenticationService
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