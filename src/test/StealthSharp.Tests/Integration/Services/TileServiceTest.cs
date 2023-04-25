#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="TailServiceTest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StealthSharp.Enumeration;
using StealthSharp.Model;
using StealthSharp.Services;
using Xunit;

namespace StealthSharp.Tests.Integration.Services;
[Trait("Category", "Integration")]
public class TileServiceTest
{
    private const string STEALTH_API_HOST = "127.0.0.1";
    private readonly ITileService _tileService;

    public TileServiceTest()
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddStealthSharp();
        serviceCollection.Configure<StealthOptions>(opt => opt.Host = STEALTH_API_HOST);
        var provider = serviceCollection.BuildServiceProvider();

        var stealth = provider.GetRequiredService<Stealth>();
        _tileService = stealth.GetStealthService<ITileService>();
        stealth.ConnectToStealthAsync().GetAwaiter().GetResult();
    }
    
    [Fact]
    public async Task GetStaticTileDataAsync()
    {
        //arrange
        var expected = new StaticTileData(){ Flags = (TileDataFlags)16464, Height = 10, Name = "log wall\0\0", Weight = 255};
        //act
        var actual = await _tileService.GetStaticTileDataAsync(160);
        //assert
        Assert.Equivalent(expected, actual);
    }
    
    [Fact]
    public async Task GetLandTileDataAsync()
    {
        //arrange
        var expected = new LandTileData(){ Flags = TileDataFlags.Impassable, Name = "dirt", TextureId = 160};
        //act
        var actual = await _tileService.GetLandTileDataAsync(160);
        //assert
        Assert.Equivalent(expected, actual);
    }
}