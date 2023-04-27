#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="StealthServiceTest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Model;
using StealthSharp.Services;
using Xunit;

#endregion

namespace StealthSharp.Tests.Integration.Services;

[Trait("Category", "Integration")]
public class StealthServiceTest
{
    private const string STEALTH_API_HOST = "127.0.0.1";
    private readonly IStealthService _stealthService;

    public StealthServiceTest()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddStealthSharp();
        serviceCollection.Configure<StealthOptions>(opt => opt.Host = STEALTH_API_HOST);
        var provider = serviceCollection.BuildServiceProvider();

        var stealth = provider.GetRequiredService<Stealth>();
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
        var expected = "1";
        //act
        var actual = await _stealthService.GetProfileNameAsync();
        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetProfileShardNameAsync()
    {
        //arrange
        var expected = "Zuluhotel.com";
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
            GitRevision = "3adbcabc",
            GitRevNumber = 1422,
            StealthVersion = new ushort[] { 9, 6, 1 }
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
        var expected = @"C:\uo\stealth\";
        //act
        var actual = await _stealthService.GetStealthPathAsync();
        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetStealthProfilePathAsync()
    {
        //arrange
        var expected = @"C:\uo\stealth\";
        //act
        var actual = await _stealthService.GetStealthProfilePathAsync();
        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetShardNameAsync()
    {
        //arrange
        var expected = "Zuluhotel.com";
        //act
        var actual = await _stealthService.GetShardNameAsync();
        //assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetShardPathAsync()
    {
        //arrange
        var expected = @"C:\Users\mmasl\AppData\Roaming\Stealth\Zuluhotel.com\";
        //act
        var actual = await _stealthService.GetShardPathAsync();
        //assert
        Assert.Equal(expected, actual);
    }
}