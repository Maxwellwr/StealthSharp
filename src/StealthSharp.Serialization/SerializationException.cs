using System;
using System.Runtime.Serialization;

namespace StealthSharp.Serialization
{
    internal class SerializationException: Exception
    {
        private SerializationException(string? message) : base(message)
        {
        }

        internal static SerializationException SerializerSequenceViolated() =>
            new SerializationException($"Sequence violated in {nameof(TcpDataAttribute.Index)}");

        internal static SerializationException SerializerLengthOutOfRange(string propertyName, string valueLength, string attributeLength) =>
            new SerializationException($"({propertyName}, {valueLength} bytes) is greater than attribute length {attributeLength} bytes");

        internal static SerializationException PropertyArgumentIsNull(string propertyName) =>
            new SerializationException($"NULL value cannot be converted ({propertyName})");

        internal static SerializationException PropertyCanReadWrite(string type, string attributeType, string attributeIndex = null) =>
            new SerializationException($"Set and Get keywords required for Serialization. Type: {type}, {nameof(TcpDataType)}: {attributeType}, {(attributeType == nameof(TcpDataType.MetaData) ? $"Index: {attributeIndex}" : null)}");

        internal static SerializationException ConverterNotFoundType(string propertyName) =>
            new SerializationException($"Converter not found for {propertyName}");

        internal static SerializationException ConverterUnknownError(string propertyName, string errorMessage) =>
            new SerializationException($"Error while trying convert data {propertyName}, error: {errorMessage}");

        internal static SerializationException AttributesRequired(string type) =>
            new SerializationException($"{type} does not have any {nameof(TcpDataAttribute)}");

        internal static SerializationException AttributeLengthRequired(string type, string attribute) =>
            new SerializationException($"In {type} {nameof(TcpDataType)}.{attribute} could not work without {nameof(TcpDataType)}.{nameof(TcpDataType.Length)}");

        internal static SerializationException AttributeRequiredWithLength(string type) =>
            new SerializationException($"In {type} {nameof(TcpDataType)}.{nameof(TcpDataType.Length)} could not work without {nameof(TcpDataType)}.{nameof(TcpDataType.Body)} or {nameof(TcpDataType)}.{nameof(TcpDataType.Compose)}");

        internal static SerializationException AttributeDuplicate(string type, string attributeType) =>
            new SerializationException($"{type} could not work with multiple {attributeType}");

        internal static SerializationException SerializerBodyPropertyIsNull() =>
            new SerializationException($"Value of {nameof(TcpDataType)}.{nameof(TcpDataType.Body)} is Null");

        internal static SerializationException SerializerComposePropertyIsNull() =>
            new SerializationException($"Value of {nameof(TcpDataType)}.{nameof(TcpDataType.Compose)} is Null");

        internal static SerializationException AttributeBodyAndComposeViolated(string type) =>
            new SerializationException($"In {type} found {nameof(TcpDataType)}.{nameof(TcpDataType.Body)} and {nameof(TcpDataType)}.{nameof(TcpDataType.Compose)} at the same time");
    }
}