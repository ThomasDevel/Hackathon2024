namespace Hackathon2024.Tests
{
    using System.IO;
    using Xunit;

    public class TestTemplateRendering
    {
        [Fact]
        public void GivenATemplateRenderedByOurOwnImplementation_WhenNormalizingAndComparingThem_TheyShouldMatch()
        {
            var templateRender = new TemplateRenderer();
            templateRender.Render();

            var givenExample = File.ReadAllText(@"..\..\..\..\Hackathon2024\result_template.html").Replace("\n", "").Replace("\r", "");
            var result = File.ReadAllText(@"..\..\..\..\Hackathon2024\bin\Debug\net8.0\result.html").Replace("\n", "").Replace("\r", "");

            var sequence1 = givenExample.Trim();
            var sequence2 = result.Trim();

            Assert.Equal(sequence1, sequence2);
        }
    }
}
