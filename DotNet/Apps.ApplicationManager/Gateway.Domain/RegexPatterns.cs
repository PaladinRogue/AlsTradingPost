namespace PaladinRogue.Gateway.Domain
{
    public static class RegexPatterns
    {
        public const string HttpsUri = "^(https://www.|https://)+[a-z0-9]+([\\-.]{1}[a-z0-9]+)*.[a-z]{2,5}(:[0-9]{1,5})?(/.*)?$";
    }
}