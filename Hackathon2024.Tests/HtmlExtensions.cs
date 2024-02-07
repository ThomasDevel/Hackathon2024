using System.Text.RegularExpressions;

namespace Hackathon2024.Tests
{
    public static class HtmlExtensions
    {
        public static Regex Whitespace = new(@"\s+", RegexOptions.Compiled);

        public static string NaiveHtmlNormalize(this string s) =>
            Whitespace.Replace(s, " ").Trim();
    }
}
