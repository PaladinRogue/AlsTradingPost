namespace ApplicationManager.Api
{
    public static class RouteDictionary
    {
        public const string Status = "Status";

        public const string ForgotPasswordResourceTemplate = "ForgotPasswordResourceTemplate";
        public const string ForgotPassword = "ForgotPassword";
        public const string ResetPasswordResourceTemplate = "ResetPasswordResourceTemplate";
        public const string ResetPassword = "ResetPassword";
        public const string GetPasswordIdentity = "GetPasswordIdentity";
        public const string ChangePasswordResourceTemplate = "ChangePasswordResourceTemplate";
        public const string ChangePassword = "ChangePassword";
        public const string RegisterPasswordResourceTemplate = "RegisterPasswordResourceTemplate";
        public const string RegisterPassword = "RegisterPassword";
        public const string ConfirmIdentityResourceTemplate = "ConfirmIdentityResourceTemplate";
        public const string ConfirmIdentity = "ConfirmIdentity";
        public const string CreateRefreshToken = "CreateRefreshToken";
        public const string Logout = "Logout";

        public const string AuthenticatePasswordResourceTemplate = "AuthenticatePasswordResourceTemplate";
        public const string AuthenticatePassword = "AuthenticatePassword";
        public const string AuthenticateRefreshTokenResourceTemplate = "AuthenticateRefreshTokenResourceTemplate";
        public const string AuthenticateRefreshToken = "AuthenticateRefreshToken";

        public const string AuthenticationServiceResourceTemplate = "AuthenticationServiceResourceTemplate";
        public const string CreateAuthenticationService = "CreateAuthenticationService";
        public const string GetAuthenticationService = "GetAuthenticationService";
    }
}