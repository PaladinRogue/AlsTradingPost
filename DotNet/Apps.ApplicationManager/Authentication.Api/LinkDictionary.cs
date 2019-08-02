namespace Authentication.Api
{
    public static class LinkDictionary
    {
        public const string Identity = nameof(Identity);
        public const string Register = nameof(Register);
        public const string ResendConfirmIdentity = nameof(ResendConfirmIdentity);
        public const string ConfirmIdentity = nameof(ConfirmIdentity);
        public const string ChangePassword = nameof(ChangePassword);
        public const string ResetPassword = nameof(ResetPassword);
        public const string ForgotPassword = nameof(ForgotPassword);
        public const string RefreshToken = nameof(RefreshToken);

        public const string Authenticate = nameof(Authenticate);
        public const string Logout = nameof(Logout);

        public const string AuthenticationServices = nameof(AuthenticationServices);
    }
}