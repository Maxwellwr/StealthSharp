#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="TailServiceTest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
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
        var expected = new StaticTileData(){ Flags = (TileDataFlags)16464, Height = 10, Name = "log wall", Weight = 255};
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

    [Fact]
    public async Task GetMapCellAsync()
    {
        //arrange
        var expected = new MapCell(198, 0);
        //act
        var actual = await _tileService.GetMapCellAsync(new WorldPoint(1024,1024), 0);
        //assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task GetLandTilesArrayAsync()
    {
        //arrange
        var expected = JsonSerializer.Deserialize<List<FoundTile>>("[{\"Tile\":170,\"X\":4278,\"Y\":2783,\"Z\":-5},{\"Tile\":169,\"X\":4278,\"Y\":2784,\"Z\":-5},{\"Tile\":168,\"X\":4278,\"Y\":2785,\"Z\":-5},{\"Tile\":170,\"X\":4278,\"Y\":2786,\"Z\":-5},{\"Tile\":171,\"X\":4278,\"Y\":2787,\"Z\":-5},{\"Tile\":168,\"X\":4278,\"Y\":2788,\"Z\":-5},{\"Tile\":170,\"X\":4278,\"Y\":2789,\"Z\":-5},{\"Tile\":168,\"X\":4278,\"Y\":2790,\"Z\":-5},{\"Tile\":168,\"X\":4278,\"Y\":2791,\"Z\":-5},{\"Tile\":169,\"X\":4278,\"Y\":2792,\"Z\":-5},{\"Tile\":169,\"X\":4278,\"Y\":2793,\"Z\":-5},{\"Tile\":168,\"X\":4279,\"Y\":2783,\"Z\":-5},{\"Tile\":170,\"X\":4279,\"Y\":2784,\"Z\":-5},{\"Tile\":169,\"X\":4279,\"Y\":2785,\"Z\":-5},{\"Tile\":169,\"X\":4279,\"Y\":2786,\"Z\":-5},{\"Tile\":168,\"X\":4279,\"Y\":2787,\"Z\":-5},{\"Tile\":171,\"X\":4279,\"Y\":2788,\"Z\":-5},{\"Tile\":171,\"X\":4279,\"Y\":2789,\"Z\":-5},{\"Tile\":170,\"X\":4279,\"Y\":2790,\"Z\":-5},{\"Tile\":168,\"X\":4279,\"Y\":2791,\"Z\":-5},{\"Tile\":171,\"X\":4279,\"Y\":2792,\"Z\":-5},{\"Tile\":170,\"X\":4279,\"Y\":2793,\"Z\":-5},{\"Tile\":171,\"X\":4280,\"Y\":2783,\"Z\":-5},{\"Tile\":169,\"X\":4280,\"Y\":2784,\"Z\":-5},{\"Tile\":168,\"X\":4280,\"Y\":2785,\"Z\":-5},{\"Tile\":171,\"X\":4280,\"Y\":2786,\"Z\":-5},{\"Tile\":168,\"X\":4280,\"Y\":2787,\"Z\":-5},{\"Tile\":170,\"X\":4280,\"Y\":2788,\"Z\":-5},{\"Tile\":168,\"X\":4280,\"Y\":2789,\"Z\":-5},{\"Tile\":168,\"X\":4280,\"Y\":2790,\"Z\":-5},{\"Tile\":169,\"X\":4280,\"Y\":2791,\"Z\":-5},{\"Tile\":168,\"X\":4280,\"Y\":2792,\"Z\":-5},{\"Tile\":168,\"X\":4280,\"Y\":2793,\"Z\":-5},{\"Tile\":170,\"X\":4281,\"Y\":2783,\"Z\":-5},{\"Tile\":171,\"X\":4281,\"Y\":2784,\"Z\":-5},{\"Tile\":171,\"X\":4281,\"Y\":2785,\"Z\":-5},{\"Tile\":170,\"X\":4281,\"Y\":2786,\"Z\":-5},{\"Tile\":168,\"X\":4281,\"Y\":2787,\"Z\":-5},{\"Tile\":168,\"X\":4281,\"Y\":2788,\"Z\":-5},{\"Tile\":169,\"X\":4281,\"Y\":2789,\"Z\":-5},{\"Tile\":170,\"X\":4281,\"Y\":2790,\"Z\":-5},{\"Tile\":171,\"X\":4281,\"Y\":2791,\"Z\":-5},{\"Tile\":171,\"X\":4281,\"Y\":2792,\"Z\":-5},{\"Tile\":171,\"X\":4281,\"Y\":2793,\"Z\":-5},{\"Tile\":170,\"X\":4282,\"Y\":2783,\"Z\":-5},{\"Tile\":168,\"X\":4282,\"Y\":2784,\"Z\":-5},{\"Tile\":170,\"X\":4282,\"Y\":2785,\"Z\":-5},{\"Tile\":170,\"X\":4282,\"Y\":2786,\"Z\":-5},{\"Tile\":168,\"X\":4282,\"Y\":2787,\"Z\":-5},{\"Tile\":168,\"X\":4282,\"Y\":2788,\"Z\":-5},{\"Tile\":168,\"X\":4282,\"Y\":2789,\"Z\":-5},{\"Tile\":168,\"X\":4282,\"Y\":2790,\"Z\":-5},{\"Tile\":171,\"X\":4282,\"Y\":2791,\"Z\":-5},{\"Tile\":168,\"X\":4282,\"Y\":2792,\"Z\":-5},{\"Tile\":169,\"X\":4282,\"Y\":2793,\"Z\":-5},{\"Tile\":171,\"X\":4283,\"Y\":2783,\"Z\":-5},{\"Tile\":171,\"X\":4283,\"Y\":2784,\"Z\":-5},{\"Tile\":171,\"X\":4283,\"Y\":2785,\"Z\":-5},{\"Tile\":169,\"X\":4283,\"Y\":2786,\"Z\":-5},{\"Tile\":171,\"X\":4283,\"Y\":2787,\"Z\":-5},{\"Tile\":169,\"X\":4283,\"Y\":2788,\"Z\":-5},{\"Tile\":171,\"X\":4283,\"Y\":2789,\"Z\":-5},{\"Tile\":169,\"X\":4283,\"Y\":2790,\"Z\":-5},{\"Tile\":170,\"X\":4283,\"Y\":2791,\"Z\":-5},{\"Tile\":168,\"X\":4283,\"Y\":2792,\"Z\":-5},{\"Tile\":171,\"X\":4283,\"Y\":2793,\"Z\":-5},{\"Tile\":168,\"X\":4284,\"Y\":2783,\"Z\":-5},{\"Tile\":170,\"X\":4284,\"Y\":2784,\"Z\":-5},{\"Tile\":170,\"X\":4284,\"Y\":2785,\"Z\":-5},{\"Tile\":169,\"X\":4284,\"Y\":2786,\"Z\":-5},{\"Tile\":171,\"X\":4284,\"Y\":2787,\"Z\":-5},{\"Tile\":171,\"X\":4284,\"Y\":2788,\"Z\":-5},{\"Tile\":168,\"X\":4284,\"Y\":2789,\"Z\":-5},{\"Tile\":168,\"X\":4284,\"Y\":2790,\"Z\":-5},{\"Tile\":170,\"X\":4284,\"Y\":2791,\"Z\":-5},{\"Tile\":169,\"X\":4284,\"Y\":2792,\"Z\":-5},{\"Tile\":168,\"X\":4284,\"Y\":2793,\"Z\":-5},{\"Tile\":168,\"X\":4285,\"Y\":2783,\"Z\":-5},{\"Tile\":169,\"X\":4285,\"Y\":2784,\"Z\":-5},{\"Tile\":171,\"X\":4285,\"Y\":2785,\"Z\":-5},{\"Tile\":169,\"X\":4285,\"Y\":2786,\"Z\":-5},{\"Tile\":168,\"X\":4285,\"Y\":2787,\"Z\":-5},{\"Tile\":171,\"X\":4285,\"Y\":2788,\"Z\":-5},{\"Tile\":171,\"X\":4285,\"Y\":2789,\"Z\":-5},{\"Tile\":169,\"X\":4285,\"Y\":2790,\"Z\":-5},{\"Tile\":170,\"X\":4285,\"Y\":2791,\"Z\":-5},{\"Tile\":169,\"X\":4285,\"Y\":2792,\"Z\":-5},{\"Tile\":169,\"X\":4285,\"Y\":2793,\"Z\":-5},{\"Tile\":171,\"X\":4286,\"Y\":2783,\"Z\":-5},{\"Tile\":171,\"X\":4286,\"Y\":2784,\"Z\":-5},{\"Tile\":170,\"X\":4286,\"Y\":2785,\"Z\":-5},{\"Tile\":170,\"X\":4286,\"Y\":2786,\"Z\":-5},{\"Tile\":169,\"X\":4286,\"Y\":2787,\"Z\":-5},{\"Tile\":169,\"X\":4286,\"Y\":2788,\"Z\":-5},{\"Tile\":171,\"X\":4286,\"Y\":2789,\"Z\":-5},{\"Tile\":170,\"X\":4286,\"Y\":2790,\"Z\":-5},{\"Tile\":171,\"X\":4286,\"Y\":2791,\"Z\":-5},{\"Tile\":168,\"X\":4286,\"Y\":2792,\"Z\":-5},{\"Tile\":169,\"X\":4286,\"Y\":2793,\"Z\":-5},{\"Tile\":170,\"X\":4287,\"Y\":2783,\"Z\":-5},{\"Tile\":171,\"X\":4287,\"Y\":2784,\"Z\":-5},{\"Tile\":169,\"X\":4287,\"Y\":2785,\"Z\":-5},{\"Tile\":168,\"X\":4287,\"Y\":2786,\"Z\":-5},{\"Tile\":170,\"X\":4287,\"Y\":2787,\"Z\":-5},{\"Tile\":169,\"X\":4287,\"Y\":2788,\"Z\":-5},{\"Tile\":170,\"X\":4287,\"Y\":2789,\"Z\":-5},{\"Tile\":171,\"X\":4287,\"Y\":2790,\"Z\":-5},{\"Tile\":170,\"X\":4287,\"Y\":2791,\"Z\":-5},{\"Tile\":169,\"X\":4287,\"Y\":2792,\"Z\":-5},{\"Tile\":168,\"X\":4287,\"Y\":2793,\"Z\":-5},{\"Tile\":168,\"X\":4288,\"Y\":2783,\"Z\":-5},{\"Tile\":168,\"X\":4288,\"Y\":2784,\"Z\":-5},{\"Tile\":168,\"X\":4288,\"Y\":2785,\"Z\":-5},{\"Tile\":170,\"X\":4288,\"Y\":2786,\"Z\":-5},{\"Tile\":171,\"X\":4288,\"Y\":2787,\"Z\":-5},{\"Tile\":171,\"X\":4288,\"Y\":2788,\"Z\":-5},{\"Tile\":171,\"X\":4288,\"Y\":2789,\"Z\":-5},{\"Tile\":168,\"X\":4288,\"Y\":2790,\"Z\":-5},{\"Tile\":168,\"X\":4288,\"Y\":2791,\"Z\":-5},{\"Tile\":171,\"X\":4288,\"Y\":2792,\"Z\":-5},{\"Tile\":168,\"X\":4288,\"Y\":2793,\"Z\":-5}]");
        //act
        var actual = await _tileService.GetLandTilesArrayAsync(
            new WorldRect(new WorldPoint(4278, 2783 ), new WorldPoint (4288,2793) ), 0, 
            new ushort[]{168, 169, 170, 171, 310, 311});
        //assert
        Assert.Equal(expected, actual);
    }
}