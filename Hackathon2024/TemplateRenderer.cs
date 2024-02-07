namespace Hackathon2024
{
    using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public struct KnownExpressions
    {
        public const string ItemValue = "itemValue";
        public const string Resource = "resource";
    }

    public class TemplateRenderer
    {
        private static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> DataSources = new()
        {
            {
                "hackathon_jury",
                new Dictionary<string, Dictionary<string, string>>
                {
                    {
                        "1",
                        new Dictionary<string, string> {
                            {"JURY_MEM", "Francois Mercer"},
                            {"LANGUAGE", "French"},
                            {"ID", "1"}
                        }
                    },
                    {
                        "2",
                        new Dictionary<string, string> {
                            {"JURY_MEM", "Jonathan Patterson"},
                            {"LANGUAGE", "English"},
                            {"ID", "2"}
                        }
                    },
                    {
                        "3",
                        new Dictionary<string, string> {
                            {"JURY_MEM", "Estelle Darcy"},
                            {"LANGUAGE", "English"},
                            {"ID", "3"}
                        }
                    }
                }
            }
        };

        private static readonly Dictionary<string, string> Resources = new()
        {
            {"heo/hackathon_2024/arrow_left.png", "https://paranadigital.selligent.com/images/SMC/heo/hackathon_2024/arrow_left.png" },
            {"heo/hackathon_2024/arrow_down.png","https://paranadigital.selligent.com/images/SMC/heo/hackathon_2024/arrow_down.png" },
            {"heo/hackathon_2024/Hackathon_2024_Banner.png", "https://paranadigital.selligent.com/images/SMC/heo/hackathon_2024/Hackathon_2024_Banner.png" }
        };

        public void Render()
        {
            var outDoc = RenderTemplate("template.html");
        }

        public HtmlDocument RenderTemplate(string documentPath)
        {
            var document = new HtmlDocument();
            document.Load(documentPath);

            HtmlNode[] repeaterNodes = document.DocumentNode.SelectNodes("//*[name()='sg:repeater']")?.ToArray() ?? [];
            foreach (var repeaterNode in repeaterNodes)
            {
                HtmlNode[] repeaterItemNodes = repeaterNode.SelectNodes("//*[name()='sg:repeateritem']")?.ToArray() ?? [];
                // hackathon_jury is the data source, JURY_MEM the field selection
                // dataselection="hackathon_jury"
                foreach (var repeaterItemNode in repeaterItemNodes)
                {
                    var innerText = repeaterItemNode.InnerText.Trim();
                    var dataSelection = repeaterNode.Attributes["dataselection"].Value;
                    var expressions = ExpressionTransformer.ParseContent(innerText).ToArray();
                    var parts = GetItemValue(dataSelection, expressions[0]);
                    string formattedResult = string.Join(",\n", parts);
                    var parentNode = repeaterNode.ParentNode;
                    repeaterNode.Remove();
                    parentNode.InnerHtml = $"{parentNode.InnerHtml}\n{formattedResult}";
                }
            }

            HtmlNode[] imageNodes = document.DocumentNode.SelectNodes("//img")?.ToArray() ?? [];
            foreach (var imageNode in imageNodes)
            {
                var srcAttributeValue = imageNode.Attributes["src"].Value;
                var expressions = ExpressionTransformer.ParseContent(srcAttributeValue)?.ToArray() ?? [];
                var actualLink = GetResourceValue(expressions[0]);
                imageNode.Attributes["src"].Value = actualLink;
            }

            File.WriteAllText(@".\result.html", document.DocumentNode.OuterHtml);

            return document;
        }

        public static string[] GetItemValue(string dataSelection, string itemValueExpression)
        {
            var lookupKey = ExpressionTransformer.ParseParenthesisContent(KnownExpressions.ItemValue, itemValueExpression);
            return DataSources[dataSelection].Values.Where(x => x.ContainsKey(lookupKey))
                                                    .Select(y => y[lookupKey]).ToArray();
        }

        public static string GetResourceValue(string resourceValueExpression)
        {
            var lookupKey = ExpressionTransformer.ParseParenthesisContent(KnownExpressions.Resource, resourceValueExpression);
            if (Resources.TryGetValue(lookupKey, out string value))
            {
                return value;
            }

            return string.Empty;
        }
    }
}

