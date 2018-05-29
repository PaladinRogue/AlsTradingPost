﻿using System.Threading.Tasks;
using AlsTradingPost.Application.Authentication.Authorisation;
using AlsTradingPost.Application.Authentication.Interfaces;
using AlsTradingPost.Application.Authentication.Models;
using Common.Application.Authentication;
using Common.Application.Authorisation;

namespace AlsTradingPost.Application.Authentication
{
    public class AuthenticationSecurityApplicationService : ISecure<IAuthenticationApplicationService>,
        IAuthenticationApplicationService
    {
        private readonly ISecurityApplicationService _securityApplicationService;
        private readonly IAuthenticationApplicationService _authenticationApplicationService;

        public AuthenticationSecurityApplicationService(
            ISecurityApplicationService securityApplicationService,
            IAuthenticationApplicationService authenticationApplicationService)
        {
            _authenticationApplicationService = authenticationApplicationService;
            _securityApplicationService = securityApplicationService;
        }

        public IAuthenticationApplicationService Service => this;

        public Task<JwtAdto> LoginAsync(LoginAdto loginAdto)
        {
            return _securityApplicationService.Secure(() => _authenticationApplicationService.LoginAsync(loginAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authentication, AuthenticationAuthorisationAction.Login));
        }

        public Task<JwtAdto> RefreshTokenAsync(RefreshTokenAdto refreshTokenAdto)
        {
            return _securityApplicationService.Secure(
                () => _authenticationApplicationService.RefreshTokenAsync(refreshTokenAdto),
                DefaultAuthorisationContext.Create(AuthorisationResource.Authentication, AuthenticationAuthorisationAction.RefreshToken));
        }
    }
}