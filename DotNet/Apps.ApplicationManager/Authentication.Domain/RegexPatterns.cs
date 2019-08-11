namespace PaladinRogue.Authentication.Domain
{
    public static class RegexPatterns
    {
        public const string Password = "^(?=.*[a-z]+.*)(?=.*[A-Z]+.*)(?=.*\\d+.*)(?:.*[!£$%^&*()_+\\-=[\\]{}'#@~,.<>?]+.*)$";
    }
}