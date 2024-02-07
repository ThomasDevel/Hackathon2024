using System.IO;
using System.Text.Json;
using Xunit;
using Data = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>[]>;

namespace Hackathon2024.Tests
{
    public class TestTemplateRendering
    {
        [Fact]
        public void GivenATemplateRenderedByOurOwnImplementation_WhenNormalizingAndComparingThem_TheyShouldMatch()
        {
            using var template = File.OpenText("template.html");
            using var dataJson = File.OpenRead("data.json");
            var data = JsonSerializer.Deserialize<Data>(dataJson);

            using var output = new StringWriter();

            new TemplateRenderer()
                .RenderTemplate(template, output, data);

            var templateContent = File.ReadAllText("result_template.html");

            Assert.Equal(
                templateContent.NaiveHtmlNormalize(),
                output.ToString().NaiveHtmlNormalize());
        }
    }
}
