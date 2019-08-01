namespace Authentication.Api
{
    public static class RouteDictionary
    {
        public const string Status = nameof(Status);

        public const string GetIdentity = nameof(GetIdentity);
        public const string ForgotPasswordResourceTemplate = nameof(ForgotPasswordResourceTemplate);
        public const string ForgotPassword = nameof(ForgotPassword);
        public const string ResetPasswordResourceTemplate = nameof(ResetPasswordResourceTemplate);
        public const string ResetPassword = nameof(ResetPassword);
        public const string ChangePasswordResourceTemplate = nameof(ChangePasswordResourceTemplate);
        public const string ChangePassword = nameof(ChangePassword);
        public const string RegisterPasswordResourceTemplate = nameof(RegisterPasswordResourceTemplate);
        public const string RegisterPassword = nameof(RegisterPassword);
        public const string ConfirmIdentityResourceTemplate = nameof(ConfirmIdentityResourceTemplate);
        public const string ConfirmIdentity = nameof(ConfirmIdentity);
        public const string CreateRefreshToken = nameof(CreateRefreshToken);
        public const string ResendConfirmIdentity = nameof(ResendConfirmIdentity);
        public const string Logout = nameof(Logout);

        public const string AuthenticatePasswordResourceTemplate = nameof(AuthenticatePasswordResourceTemplate);
        public const string AuthenticatePassword = nameof(AuthenticatePassword);
        public const string AuthenticateRefreshTokenResourceTemplate = nameof(AuthenticateRefreshTokenResourceTemplate);
        public const string AuthenticateRefreshToken = nameof(AuthenticateRefreshToken);
        public const string AuthenticateFacebookResourceTemplate = nameof(AuthenticateFacebookResourceTemplate);
        public const string AuthenticateFacebook = nameof(AuthenticateFacebook);
        public const string AuthenticateGoogleResourceTemplate = nameof(AuthenticateGoogleResourceTemplate);
        public const string AuthenticateGoogle = nameof(AuthenticateGoogle);

        public const string GetAuthenticationServices = nameof(GetAuthenticationServices);
        public const string GetAuthenticationServiceResourceTemplateTypes = nameof(GetAuthenticationServiceResourceTemplateTypes);
        public const string FacebookAuthenticationServiceResourceTemplate = nameof(FacebookAuthenticationServiceResourceTemplate);
        public const string CreateFacebookAuthenticationService = nameof(CreateFacebookAuthenticationService);
        public const string GetFacebookAuthenticationService = nameof(GetFacebookAuthenticationService);
        public const string ChangeFacebookAuthenticationService = nameof(ChangeFacebookAuthenticationService);
        public const string DeleteFacebookAuthenticationService = nameof(DeleteFacebookAuthenticationService);
        public const string GoogleAuthenticationServiceResourceTemplate = nameof(GoogleAuthenticationServiceResourceTemplate);
        public const string CreateGoogleAuthenticationService = nameof(CreateGoogleAuthenticationService);
        public const string GetGoogleAuthenticationService = nameof(GetGoogleAuthenticationService);
        public const string ChangeGoogleAuthenticationService = nameof(ChangeGoogleAuthenticationService);
        public const string DeleteGoogleAuthenticationService = nameof(DeleteGoogleAuthenticationService);
    }
}