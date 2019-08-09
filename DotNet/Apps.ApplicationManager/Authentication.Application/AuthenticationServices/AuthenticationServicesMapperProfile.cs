using Authentication.Application.AuthenticationServices.Models;
using Authentication.Application.AuthenticationServices.Models.Facebook;
using Authentication.Application.AuthenticationServices.Models.Google;
using Authentication.Domain.AuthenticationServices;
using AutoMapper;

namespace Authentication.Application.AuthenticationServices
{
    public class AuthenticationServicesMapperProfile : Profile
    {
        public AuthenticationServicesMapperProfile()
        {
            CreateMap<AuthenticationService, AuthenticationServiceAdto>()
                .Include<AuthenticationGrantTypePassword, PasswordAuthenticationServiceAdto>()
                .Include<AuthenticationGrantTypeRefreshToken, RefreshTokenAuthenticationServiceAdto>();

            CreateMap<AuthenticationGrantTypeClientCredential, ClientCredentialAuthenticationServiceAdto>()
                .Include<AuthenticationGrantTypeFacebook, FacebookAuthenticationServiceAdto>()
                .Include<AuthenticationGrantTypeGoogle, GoogleAuthenticationServiceAdto>();

            CreateMap<AuthenticationGrantTypePassword, PasswordAuthenticationServiceAdto>();
            CreateMap<AuthenticationGrantTypeRefreshToken, RefreshTokenAuthenticationServiceAdto>();
            CreateMap<AuthenticationGrantTypeFacebook, FacebookAuthenticationServiceAdto>();
            CreateMap<AuthenticationGrantTypeGoogle, GoogleAuthenticationServiceAdto>();
        }
    }
}