namespace Authentication.Api
{
    public static class RouteDictionary
    {
        public const string Status = nameof(Status);

        public const string ForgotPasswordResourceTemplate = nameof(ForgotPasswordResourceTemplate);
        public const string ForgotPassword = nameof(ForgotPassword);
        public const string ResetPasswordResourceTemplate = nameof(ResetPasswordResourceTemplate);
        public const string ResetPassword = nameof(ResetPassword);
        public const string GetPasswordIdentity = nameof(GetPasswordIdentity);
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
        public const string AuthenticateClientCredentialResourceTemplate = nameof(AuthenticateClientCredentialResourceTemplate);
        public const string AuthenticateClientCredential = nameof(AuthenticateClientCredential);

        public const string GetAuthenticationServices = nameof(GetAuthenticationServices);
        public const string AuthenticationServiceResourceTemplate = nameof(AuthenticationServiceResourceTemplate);
        public const string CreateAuthenticationService = nameof(CreateAuthenticationService);
        public const string GetAuthenticationService = nameof(GetAuthenticationService);
        public const string ChangeAuthenticationService = nameof(ChangeAuthenticationService);
    }
}