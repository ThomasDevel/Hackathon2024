namespace Hackathon2024.Tests
{
    using System.IO;
    using System.Text.Json;
    using Xunit;
    using Data = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>[]>;


    public class TestTemplateRendering
    {
        [Theory]
        [InlineData("template.html", "result_template.html")]
        [InlineData("template_participants.html", "result_template_participants.html")]
        [InlineData("template_participants_no_repeater.html", "result_template_participants.html")]
        [InlineData("template_participants_no_resources.html", "result_template_participants.html")]
        public void GivenATemplateRenderedByOurOwnImplementation_WhenNormalizingAndComparingThem_TheyShouldMatch(string inputTemplate, string outputTemplate)
        {
            using var template = File.OpenText(inputTemplate);
            using var dataJson = File.OpenRead("data.json");
            var data = JsonSerializer.Deserialize<Data>(dataJson);

            using var output = new StringWriter();

            new TemplateRenderer()
                .RenderTemplate(template, output, data);

            var templateContent = File.ReadAllText(outputTemplate);

            Assert.Equal(
                templateContent.NaiveHtmlNormalize(),
                output.ToString().NaiveHtmlNormalize());
        }
    }
}
