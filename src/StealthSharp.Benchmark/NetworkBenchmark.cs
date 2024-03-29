#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializerBenchmark.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Model;
using StealthSharp.Services;

#endregion

namespace StealthSharp.Benchmark
{
    [MarkdownExporter]
    [HtmlExporter]
    [SimpleJob(RunStrategy.Throughput, 1)]
    [MemoryDiagnoser]
    public class NetworkBenchmark
    {
        private readonly Stealth _stealth;
        private readonly IStealthService _stealthService;

        public NetworkBenchmark()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddStealthSharp();
            serviceCollection.Configure<StealthOptions>(opt => opt.Host = "127.0.0.1");
            var provider = serviceCollection.BuildServiceProvider();

            _stealth = provider.GetRequiredService<Stealth>();
            _stealthService = _stealth.GetStealthService<IStealthService>();
        }

        [GlobalSetup]
        public Task GlobalSetup()
        {
            return _stealth.ConnectToStealthAsync();
        }

        //[Benchmark]
        public Task<Model.AboutData> GetAboutPacket()
        {
            return _stealthService.GetStealthInfoAsync();
        }

        [Benchmark]
        public Task GetProfilePacket()
        {
            return _stealthService.GetProfileNameAsync();
        }

        //[Benchmark]
        public Task<GumpInfo> GetComplexGump()
        {
            return _stealth.Gump.GetGumpInfoAsync(0);
        }
    }
}