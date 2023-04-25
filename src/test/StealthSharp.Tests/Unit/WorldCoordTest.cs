#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="WorldCoordTest.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using StealthSharp.Model;
using Xunit;

namespace StealthSharp.Tests.Unit;
[Trait("Category", "Unit")]
public class WorldCoordTest
{
    public static IEnumerable<object[]> MultiplyTestDataSet => new[]
    {
        new object[] { new WorldVector(1, 2), 2f, new WorldVector(2, 4) },
        new object[] { new WorldVector(1, 2), -2f, new WorldVector(-2, -4) },
        new object[] { new WorldVector(2, 4), 0.5f, new WorldVector(1, 2) }
    };

    public static IEnumerable<object[]> MultiplyOverflowTestDataSet => new[]
    {
        new object[] { new WorldVector(short.MaxValue, 2), 2f },
        new object[] { new WorldVector(short.MaxValue, 4), -2f }
    };
    
    [Theory]
    [MemberData(nameof(MultiplyTestDataSet))]
    public void vector_multiply_should_work(WorldVector init, float k, WorldVector expected)
    {
        // arrange
        // act
        var actual = init * k;
        // assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(MultiplyOverflowTestDataSet))]
    public void vector_multiply_should_overflow(WorldVector init, float k)
    {
        // arrange
        // act
        // assert
        Assert.Throws(typeof(OverflowException),()=> init * k);
    }
}