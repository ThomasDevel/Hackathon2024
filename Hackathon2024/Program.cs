namespace Hackathon2024
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var templateRender = new TemplateRenderer();
            var doc = templateRender.RenderTemplate("template.html");
            if (doc != null)
            {
                return 0;
            }

            return -1;
        }
    }
}
