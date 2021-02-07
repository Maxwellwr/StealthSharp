#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="StealthServiceTest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Model;
using StealthSharp.Services;
using Xunit;

namespace StealthSharp.Tests.Integration.Services
{
    public class StealthServiceTest
    {
        private readonly IStealthService _stealthService;

        public StealthServiceTest()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddStealthSharp();
            serviceCollection.Configure<StealthOptions>(opt => opt.Host = "10.211.55.3");
            var provider = serviceCollection.BuildServiceProvider();

            Stealth stealth = provider.GetRequiredService<Stealth>();
            _stealthService = stealth.GetStealthService<IStealthService>();
            stealth.ConnectToStealthAsync().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task GetCurrentScriptPathAsync()
        {
            //arrange
            var expected = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //act
            var actual = await _stealthService.GetCurrentScriptPathAsync();
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetProfileNameAsync()
        {
            //arrange
            var expected = "test";
            //act
            var actual = await _stealthService.GetProfileNameAsync();
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetProfileShardNameAsync()
        {
            //arrange
            var expected = "local";
            //act
            var actual = await _stealthService.GetProfileShardNameAsync();
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetStealthInfoAsync()
        {
            //arrange
            var expected = new AboutData()
            {
                Build = 0,
                BuildDate = new DateTime(2021, 01, 27, 15, 0, 31),
                GitRevision = "566c18d9",
                GitRevNumber = 1422,
                StealthVersion = new ushort[] {8, 11, 4}
            };
            //act
            var actual = await _stealthService.GetStealthInfoAsync();
            //assert
            Assert.Equal(expected.StealthVersion, actual.StealthVersion);
            Assert.Equal(expected.GitRevision, actual.GitRevision);
        }

        [Fact]
        public async Task GetStealthPathAsync()
        {
            //arrange
            var expected = @"\\Mac\Home\Documents\stealth\";
            //act
            var actual = await _stealthService.GetStealthPathAsync();
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetStealthProfilePathAsync()
        {
            //arrange
            var expected = @"\\Mac\Home\Documents\stealth\";
            //act
            var actual = await _stealthService.GetStealthProfilePathAsync();
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetShardNameAsync()
        {
            //arrange
            var expected = "";
            //act
            var actual = await _stealthService.GetShardNameAsync();
            //assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetShardPathAsync()
        {
            //arrange
            var expected = @"C:\Users\maslich\AppData\Roaming\Stealth\\";
            //act
            var actual = await _stealthService.GetShardPathAsync();
            //assert
            Assert.Equal(expected, actual);
        }
    }
}