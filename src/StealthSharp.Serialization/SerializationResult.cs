using System;
using System.Buffers;

namespace StealthSharp.Serialization
{
    public class SerializationResult: IDisposable
    {
        private readonly ArrayPool<byte> _arrayPool;

        public byte[] RentedArray { get; }
        
        public SerializationResult(ArrayPool<byte> arrayPool, int size)
        {
            _arrayPool = arrayPool;
            RentedArray = _arrayPool.Rent(size);
        }
        
        public void Dispose()
        {
            _arrayPool.Return(RentedArray, false);
        }
    }
}