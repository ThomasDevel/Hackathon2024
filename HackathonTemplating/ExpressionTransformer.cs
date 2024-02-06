namespace HackathonTemplating
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
            new Regex(@"\[%(?<field>.*?)%\]",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        private static readonly Regex ItemValueFieldDetector =
            new Regex(@"itemValue\('(?<field>.*?)'\)",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        private static readonly Regex ResourceFieldDetector =
            new Regex(@"resource\('(?<field>.*?)'\)",
            RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled, TimeSpan.FromSeconds(15));

        public static IEnumerable<string> ParseContent(string content)
        {
            return ExpressionDetector.Matches(content).Cast<Match>().Select(match =>
            {
                var expressionGroup = match.Groups["expression"];
                if (expressionGroup.Success)
                {
                    return expressionGroup.Value;
                }

                return match.Value;
            }).ToArray();
        }

        public static string ParseParenthesisContent(string type, string expression)
        {
            var field = type switch
            {
                "itemValue" => ItemValueFieldDetector.Match(expression),
                "resource" => ResourceFieldDetector.Match(expression),
                _ => throw new NotSupportedException("This type before the parenthesis is not supported..")
            };

            var fieldGroup = field.Groups["field"];
            if (fieldGroup.Success)
            {
                return fieldGroup.Value;
            }

            return string.Empty;
        }
    }
}