namespace Hackathon2024.Tests
{
    using System.Text.RegularExpressions;

    public static class HtmlExtensions
    {
        public static Regex Whitespace = new(@"\s+", RegexOptions.Compiled);

        public static string NaiveHtmlNormalize(this string s) =>
            Whitespace.Replace(s, " ").Trim();
    }
}
