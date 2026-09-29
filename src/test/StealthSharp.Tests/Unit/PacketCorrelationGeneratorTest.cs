#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketCorrelationGeneratorTest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

#endregion

namespace StealthSharp.Tests.Unit
{
    [Trait("Category", "Unit")]
    public class PacketCorrelationGeneratorTest
    {
        [Fact]
        public void Ids_should_start_at_one_and_wrap_to_one_skipping_zero()
        {
            var generator = new PacketCorrelationGenerator();

            var ids = Enumerable.Range(0, ushort.MaxValue + 2).Select(_ => generator.GetNextCorrelationId()).ToArray();

            Assert.Equal(1, ids[0]);
            Assert.Equal(ushort.MaxValue, ids[ushort.MaxValue - 1]);
            Assert.Equal(1, ids[ushort.MaxValue]);
            Assert.DoesNotContain((ushort)0, ids);
        }

        [Fact]
        public void Concurrent_calls_should_return_unique_ids()
        {
            var generator = new PacketCorrelationGenerator();
            var ids = new ConcurrentBag<ushort>();

            Parallel.For(0, ushort.MaxValue, _ => ids.Add(generator.GetNextCorrelationId()));

            Assert.Equal(ushort.MaxValue, ids.Distinct().Count());
        }
    }
}
