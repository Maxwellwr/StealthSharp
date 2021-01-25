using System;
using BenchmarkDotNet.Running;

namespace StealthSharp.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<SerializerBenchmark>();
        }
    }
}