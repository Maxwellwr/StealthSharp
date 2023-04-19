#region Copyright

// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using BenchmarkDotNet.Running;

#endregion

namespace StealthSharp.Benchmark
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var serializer = BenchmarkRunner.Run<SerializerBenchmark>();
            var network = BenchmarkRunner.Run<NetworkBenchmark>();
            //var waitingDictionary = BenchmarkRunner.Run<WaitingDictionaryBenchmark>();
        }
    }
}