namespace Hackathon2024
{
    using System.IO;

    internal class Program
    {
        static int Main(string[] args)
        {
            var templateRender = new TemplateRenderer();
            var doc = templateRender.RenderTemplate("template.html");
            if (doc != null)
            {
                File.WriteAllText(@".\result.html", doc.DocumentNode.OuterHtml);
                return 0;
            }

            return -1;
        }
    }
}
