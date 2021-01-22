using System;

namespace StealthSharp.Serialization
{
    public class SerializationOptions
    {
        public const string ConfigSection = "StealthSharp:Serializer";
        
        public Type ArrayCountType { get; set; } = typeof(int);
    }
}