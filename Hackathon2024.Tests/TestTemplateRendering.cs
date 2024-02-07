namespace Hackathon2024.Tests
{
    using System.IO;
    using Xunit;

    public class TestTemplateRendering
    {
        [Fact]
        public void GivenATemplateRenderedByOurOwnImplementation_WhenNormalizingAndComparingThem_TheyShouldMatch()
        {
            using var template = File.OpenText("template.html");
            using var output = new StringWriter();

            new TemplateRenderer()
                .RenderTemplate(template, output);

            var templateContent = File.ReadAllText("result_template.html");

            Assert.Equal(
                templateContent.NaiveHtmlNormalize(),
                output.ToString().NaiveHtmlNormalize());
        }
    }
}
