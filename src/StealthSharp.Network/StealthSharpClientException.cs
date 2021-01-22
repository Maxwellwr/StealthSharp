using System;

namespace StealthSharp.Network
{
    public class StealthSharpClientException : Exception
    {
        private StealthSharpClientException(string message) : base(message)
        {
        }

        public static StealthSharpClientException ConverterError(string converterName) => new($"Converter {converterName} does not have generic type");
        
        public static StealthSharpClientException ConnectionBroken() => new("Connection was broken");
    }
}