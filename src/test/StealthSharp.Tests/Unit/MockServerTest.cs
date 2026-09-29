#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MockServerTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.MockServer;
using StealthSharp.Model;
using StealthSharp.Network;
using StealthSharp.Services;
using Xunit;

#endregion

namespace StealthSharp.Tests.Unit
{
    /// <summary>
    ///     End to end tests of the real client stack against <see cref="MockStealthServer" /> over loopback TCP.
    /// </summary>
    [Trait("Category", "Unit")]
    public class MockServerTest : IDisposable
    {
        private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(10);
        private readonly MockStealthServer _server = new();
        private readonly ServiceProvider _provider;
        private readonly Stealth _stealth;

        public MockServerTest()
        {
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
        }

        [Fact]
        public async Task Connect_should_pass_handshake_and_version_check()
        {
            await Within(_stealth.ConnectToStealthAsync());

            Assert.Equal(1, _server.GetRequestCount(PacketType.SCLangVersion));
            Assert.Equal(1, _server.GetRequestCount(PacketType.SCGetStealthInfo));
        }

        [Fact]
        public async Task Simple_request_should_return_canned_string()
        {
            await Within(_stealth.ConnectToStealthAsync());

            var name = await Within(_stealth.GetStealthService<IStealthService>().GetProfileNameAsync());

            Assert.Equal(MockDefaults.ProfileName, name);
        }

        [Fact]
        public async Task Sequential_requests_should_be_matched_by_correlation_id()
        {
            _server.On(PacketType.SCGetSelfID, r => (uint)r.CorrelationId);
            await Within(_stealth.ConnectToStealthAsync());

            for (var i = 0; i < 200; i++)
            {
                var id = await Within(_stealth.Char.GetSelfAsync());
                Assert.True(id != 0);
            }

            Assert.Equal(0, _server.UnhandledRequests);
        }

        [Fact]
        public async Task Sequential_requests_should_not_wait_for_nagle_and_delayed_ack()
        {
            await Within(_stealth.ConnectToStealthAsync());
            var service = _stealth.GetStealthService<IStealthService>();
            await Within(service.GetProfileNameAsync());

            const int count = 200;
            var stopwatch = Stopwatch.StartNew();
            for (var i = 0; i < count; i++)
                await Within(service.GetProfileNameAsync());
            stopwatch.Stop();

            // A packet written as several small segments costs ~40 ms per request (8+ s here) on Linux loopback,
            // a single flushed write with NoDelay takes ~2 ms. The limit is generous to keep slow CI stable.
            Assert.True(stopwatch.Elapsed < TimeSpan.FromSeconds(4),
                $"{count} requests took {stopwatch.Elapsed.TotalMilliseconds:F0} ms");
        }

        [Fact]
        public async Task Concurrent_requests_should_not_interleave_and_get_own_responses()
        {
            // Body echoed back as the response, so every caller can check it got its own answer.
            _server.On(PacketType.SCGetSelfID, r => BitConverter.ToUInt32(r.Body.Span));
            await Within(_stealth.ConnectToStealthAsync());
            var client = _provider.GetRequiredService<IStealthSharpClient>();

            var results = await Within(Task.WhenAll(Enumerable.Range(0, 500).Select(i => Task.Run(async () =>
            {
                var echoed = await client.SendPacketAsync<uint, uint>(PacketType.SCGetSelfID, (uint)i);
                return (sent: (uint)i, echoed);
            }))));

            Assert.All(results, r => Assert.Equal(r.sent, r.echoed));
            Assert.Equal(0, _server.UnhandledRequests);
        }

        [Fact]
        public async Task Large_response_should_be_deserialized()
        {
            await Within(_stealth.ConnectToStealthAsync());

            var gump = await Within(_stealth.Gump.GetGumpInfoAsync(0));

            Assert.Equal(50, gump.ExtData.GumpButtons.Length);
            Assert.Equal(50, gump.ExtData.GumpText.Length);
        }

        [Fact]
        public async Task Server_event_should_reach_subscriber()
        {
            await Within(_stealth.ConnectToStealthAsync());
            var received = new TaskCompletionSource<SpeechEvent>();
            await Within(_stealth.GetStealthService<IEventSystemService>().OnSpeech(e => received.TrySetResult(e)));

            await _server.PushEventAsync(new ServerEventData<SpeechEvent>(EventType.Speech, new SpeechEvent
            {
                Text = "Hello", SenderName = "Mock", Sender = new Identity { Id = 7 }
            }));

            var ev = await Within(received.Task);
            Assert.Equal("Hello", ev.Text);
            Assert.Equal(7u, ev.Sender.Id);
        }

        public void Dispose()
        {
            _provider.Dispose();
            _server.Dispose();
        }

        private static async Task<T> Within<T>(Task<T> task)
        {
            return await task.WaitAsync(Timeout);
        }

        private static async Task Within(Task task)
        {
            await task.WaitAsync(Timeout);
        }
    }
}
