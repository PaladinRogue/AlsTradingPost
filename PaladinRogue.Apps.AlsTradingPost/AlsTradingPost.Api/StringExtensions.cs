namespace AlsTradingPost.Api
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
           return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
    }
}
