#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializerBenchmark.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Drenalol.WaitingDictionary;
using Microsoft.Diagnostics.Runtime.ICorDebug;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using StealthSharp.Model;
using StealthSharp.Serialization;
using StealthSharp.Services;

namespace StealthSharp.Benchmark
{
    [MarkdownExporter, HtmlExporter]
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
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
            => Task.CompletedTask;

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