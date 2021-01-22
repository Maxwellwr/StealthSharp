using System;
using System.Buffers;

namespace StealthSharp.Serialization
{
    public class SerializationResult: ISerializationResult
    {
        private readonly ArrayPool<byte> _arrayPool;
        private readonly byte[] _rentedArray;
        private readonly int _size;
        public Memory<byte> Memory => _rentedArray.AsMemory().Slice(0,_size);
        
        public SerializationResult(int size)
        {
            _arrayPool = ArrayPool<byte>.Shared;
            _rentedArray = _arrayPool.Rent(size);
            _size = size;
        }
        
        public void Dispose()
        {
            _arrayPool.Return(_rentedArray, false);
        }
    }
}