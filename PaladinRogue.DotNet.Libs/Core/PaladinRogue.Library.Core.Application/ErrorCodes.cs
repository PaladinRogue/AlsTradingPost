namespace PaladinRogue.Library.Core.Application
{
    public static class ErrorCodes
    {
        public const string InvalidLogin = nameof(InvalidLogin);
        public const string ResourceType = nameof(ResourceType);
        public const string PasswordLoginNotConfigured = nameof(PasswordLoginNotConfigured);
        public const string RefreshTokenLoginNotConfigured = nameof(RefreshTokenLoginNotConfigured);
        public const string ClientCredentialLoginNotConfigured = nameof(ClientCredentialLoginNotConfigured);
        public const string IdentityAlreadyConfirmed = nameof(IdentityAlreadyConfirmed);
    }
}