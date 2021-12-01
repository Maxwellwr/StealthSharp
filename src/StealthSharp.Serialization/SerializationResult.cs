#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializationResult.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Buffers;

namespace StealthSharp.Serialization
{
    public sealed class SerializationResult : ISerializationResult
    {
        private readonly ArrayPool<byte> _arrayPool;
        private readonly byte[] _rentedArray;
        public Memory<byte> Memory => _rentedArray.AsMemory()[..Length];
        public int Length { get; }

        public SerializationResult(int size)
        {
            _arrayPool = ArrayPool<byte>.Shared;
            _rentedArray = _arrayPool.Rent(size);
            Length = size;
        }

        public SerializationResult(ReadOnlySequence<byte> sequence)
        : this((int)sequence.Length)
        {
            sequence.CopyTo(_rentedArray);
        }

        public void Dispose()
        {
            _arrayPool.Return(_rentedArray);
        }
    }
}