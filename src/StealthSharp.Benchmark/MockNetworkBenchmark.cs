#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MockNetworkBenchmark.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.MockServer;
using StealthSharp.Model;
using StealthSharp.Services;

#endregion

namespace StealthSharp.Benchmark
{
    /// <summary>
    ///     Client round trips against <see cref="MockStealthServer" />: needs no real Stealth. Measures the client
    ///     stack (serialization, framing, sockets), not Stealth itself.
    /// </summary>
    [MemoryDiagnoser]
    public class MockNetworkBenchmark
    {
        private MockStealthServer _server = null!;
        private ServiceProvider _provider = null!;
        private Stealth _stealth = null!;
        private IStealthService _stealthService = null!;

        [GlobalSetup]
        public async Task GlobalSetup()
        {
            _server = new MockStealthServer();
            _server.Start();

            var services = new ServiceCollection();
            services.AddStealthSharp();
            services.Configure<StealthOptions>(o =>
            {
                o.Host = "127.0.0.1";
                o.Port = _server.DiscoveryPort;
            });
            _provider = services.BuildServiceProvider();
            _stealth = _provider.GetRequiredService<Stealth>();
            await _stealth.ConnectToStealthAsync();
            _stealthService = _stealth.GetStealthService<IStealthService>();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _provider.Dispose();
            _server.Dispose();
        }

        /// <summary>Small request, small string response.</summary>
        [Benchmark(Baseline = true)]
        public Task<string> GetProfileName()
        {
            return _stealthService.GetProfileNameAsync();
        }

        [Benchmark]
        public Task<Model.AboutData> GetStealthInfo()
        {
            return _stealthService.GetStealthInfoAsync();
        }

        /// <summary>Request with a body and a large nested response.</summary>
        [Benchmark]
        public Task<GumpInfo> GetGumpInfo()
        {
            return _stealth.Gump.GetGumpInfoAsync(0);
        }
    }
}
