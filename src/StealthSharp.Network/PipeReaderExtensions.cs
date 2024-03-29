#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PipeReaderExtensions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace StealthSharp.Network
{
    public static class PipeReaderExtensions
    {
        public static async Task<ReadResult> ReadDataAsync(this PipeReader reader, long length,
            CancellationToken cancellationToken = default)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException(nameof(length));

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var readResult = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);

                if (readResult.IsCanceled)
                    throw new OperationCanceledException();

                if (readResult.IsCompleted)
                    throw new EndOfStreamException();

                if (readResult.Buffer.IsEmpty)
                    continue;

                var readResultLength = readResult.Buffer.Length;

                if (readResultLength >= length)
                    return readResult;

                reader.Examine(readResult.Buffer.Start, readResult.Buffer.GetPosition(readResultLength));
            }
        }

        public static void Examine(this PipeReader reader, SequencePosition consumed, SequencePosition examined)
        {
            reader.AdvanceTo(consumed, examined);
        }

        public static void Consume(this PipeReader reader, SequencePosition consume)
        {
            reader.AdvanceTo(consume);
        }

        public static ReadOnlySequence<byte> Slice(in this ReadResult readResult, int start, int length)
        {
            return readResult.Buffer.Slice(start, length);
        }
    }
}