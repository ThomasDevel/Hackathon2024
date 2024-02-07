namespace Hackathon2024
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    /// <summary>
    /// ExpressionTransformer for Parsing Selligent specific expressions '[% %]'
    /// </summary>
    public class ExpressionTransformer
    {
        private static readonly Regex ExpressionDetector =
            new Regex(@"\[%(?<expression>.*?)%\]",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        private static readonly Regex ItemValueFieldDetector =
            new Regex(@"itemValue\('(?<field>.*?)'\)",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        private static readonly Regex ResourceFieldDetector =
            new Regex(@"resource\('(?<resource>.*?)'\)",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        public static string RenderExpressions(string content, string baseUrl, Dictionary<string, object> data = null)
        {
            return ExpressionDetector.Replace(content, expressionMatch =>
            {
                var expression = expressionMatch.Groups["expression"].Value;

                expression = ItemValueFieldDetector.Replace(expression, expressionMatch =>
                {
                    var field = expressionMatch.Groups["field"].Value;

                    return (data[field] ?? "").ToString();
                });

                expression = ResourceFieldDetector.Replace(expression, expressionMatch =>
                {
                    var resource = expressionMatch.Groups["resource"].Value;

                    return $"{baseUrl}{resource}";
                });

                return expression;
            });
        }
    }
}