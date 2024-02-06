namespace Hackathon.Tests
{
    using HackathonTemplating;
    using System.IO;
    using Xunit;

    public class TestTemplateRendering
    {
        [Fact]
        public void GivenATemplateRenderedByOurOwnImplementation_WhenNormalizingAndComparingThem_TheyShouldMatch()
        {
            var templateRender = new TemplateRenderer();
            templateRender.Render();

            var givenExample = File.ReadAllText(@"C:\Users\Taerts\source\repos\HackathonTemplating\bin\Debug\net8.0\result_template.html");
            var result = File.ReadAllText(@"C:\Users\Taerts\source\repos\HackathonTemplating\bin\Debug\net8.0\result.html");

            var sequence1 = givenExample.Trim().Replace("\t", "   ");
            var sequence2 = result.Trim().Replace("\t", "   ");

            Assert.Equal(sequence1, sequence2);
        }
    }
}
