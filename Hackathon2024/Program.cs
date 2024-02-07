using System;
using System.IO;
using System.Text.Json;
using Data = System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, object>[]>;

namespace Hackathon2024
{
    using System.IO;

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

            new TemplateRenderer()
                .RenderTemplate(template, Console.Out, data);

            return 0;
        }
    }
}
