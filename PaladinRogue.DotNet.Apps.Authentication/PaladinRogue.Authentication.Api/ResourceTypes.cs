namespace PaladinRogue.Authentication.Api
{
    public static class ResourceTypes
    {
        public const string Session = nameof(Session);

        public const string Identity = nameof(Identity);
        public const string ForgotPassword = nameof(ForgotPassword);
        public const string ResetPassword = nameof(ResetPassword);
        public const string Password = nameof(Password);
        public const string RegisterPassword = nameof(RegisterPassword);
        public const string RefreshToken = nameof(RefreshToken);
        public const string AuthenticatePassword = nameof(AuthenticatePassword);
        public const string AuthenticateRefreshToken = nameof(AuthenticateRefreshToken);
        public const string ConfirmIdentity = nameof(ConfirmIdentity);
        public const string ResendConfirmIdentity = nameof(ResendConfirmIdentity);
        public const string AuthenticateFacebook = nameof(AuthenticateFacebook);
        public const string AuthenticateGoogle = nameof(AuthenticateGoogle);

        public const string AuthenticationService = nameof(AuthenticationService);
        public const string AuthenticationServicePassword = nameof(AuthenticationServicePassword);
        public const string AuthenticationServiceFacebook = nameof(AuthenticationServiceFacebook);
        public const string AuthenticationServiceGoogle = nameof(AuthenticationServiceGoogle);
        public const string AuthenticationServiceRefreshToken = nameof(AuthenticationServiceRefreshToken);
    }
}