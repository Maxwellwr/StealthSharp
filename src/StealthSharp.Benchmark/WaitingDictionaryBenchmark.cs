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
using Drenalol.WaitingDictionary;

#endregion

namespace StealthSharp.Benchmark
{
    [MarkdownExporter]
    [HtmlExporter]
    [SimpleJob(RunStrategy.Throughput, 1)]
    [MemoryDiagnoser]
    public class WaitingDictionaryBenchmark
    {
        private readonly WaitingDictionary<ushort, object> _dictionary;


        public WaitingDictionaryBenchmark()
        {
            _dictionary = new WaitingDictionary<ushort, object>();
        }

        [GlobalSetup]
        public Task GlobalSetup()
        {
            return Task.CompletedTask;
        }

        [Benchmark]
        public Task SetAndRetrieveValueWithRestoreContext()
        {
            var waitTask = _dictionary.WaitAsync(1);
            var setTask = _dictionary.SetAsync(1, new object());
            return Task.WhenAll(waitTask, setTask);
        }

        [Benchmark]
        public async Task SetAndRetrieveValueWithoutRestoreContext()
        {
            var waitTask = _dictionary.WaitAsync(1);
            var setTask = _dictionary.SetAsync(1, new object());
            await Task.WhenAll(waitTask, setTask).ConfigureAwait(false);
        }
    }
}