using System;
using System.IO;

namespace Hackathon2024
{
    internal class Program
    {
        static int Main(string[] args)
        {
            var template = args.Length == 1
                ? File.OpenText(args[0])
                : Console.In;

            new TemplateRenderer()
                .RenderTemplate(template, Console.Out);

            return 0;
        }
    }
}
