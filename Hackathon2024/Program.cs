namespace Hackathon2024
{
    using System;
    using System.IO;
    using System.Text.Json;
    using Data = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>[]>;

    internal class Program
    {
        static int Main(string[] args)
        {
            var template = args.Length > 0
                ? File.OpenText(args[0])
                : Console.In;

            var data = JsonSerializer.Deserialize<Data>(
                args.Length > 1
                    ? File.ReadAllText(args[1])
                    : "[]");

            // Uncomment this to actually write to a file, replace Console.Out with tw
            // using TextWriter tw = new StreamWriter("result_template.html");
            new TemplateRenderer()
                .RenderTemplate(template, Console.Out, data);

            return 0;
        }
    }
}
