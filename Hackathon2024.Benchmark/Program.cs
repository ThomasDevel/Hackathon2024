namespace Hackathon2024.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Running;
    using HtmlAgilityPack;

    [MemoryDiagnoser]
    public class HackathonBenchmark
    {
        private TemplateRenderer _renderer;

        [Params(100, 500, 1000)]
        public int N { get; set; }

        [GlobalSetup]
        public void Init()
        {
            _renderer = new TemplateRenderer();
        }

        [Benchmark]
        public HtmlDocument TemplateRenderer_DefaultRender()
        {
            return _renderer.RenderTemplate(@"..\..\..\..\template.html");
        }
    }
    internal class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<HackathonBenchmark>();
        }
    }
}
