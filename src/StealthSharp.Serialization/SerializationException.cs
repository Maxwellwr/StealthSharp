using System;

namespace StealthSharp.Serialization
{
    internal class SerializationException : Exception
    {
        private SerializationException(string? message) : base(message)
        {
        }

        internal static SerializationException PropertyArgumentIsNull(string propertyName) =>
            new($"NULL value cannot be converted ({propertyName})");

        internal static SerializationException PropertyCanReadWrite(string type, string attributeType,
            string attributeIndex = null) =>
            new(
                $"Set and Get keywords required for Serialization. Type: {type}, {nameof(PacketDataType)}: {attributeType}, {(attributeType == nameof(PacketDataType.MetaData) ? $"Index: {attributeIndex}" : null)}");

        internal static SerializationException ConverterNotFoundType(string propertyName) =>
            new($"Converter not found for {propertyName}");

        internal static SerializationException AttributesRequired(string type) =>
            new($"{type} does not have any {nameof(PacketDataAttribute)}");

        internal static SerializationException AttributeLengthRequired(string type, string attribute) =>
            new(
                $"In {type} {nameof(PacketDataType)}.{attribute} could not work without {nameof(PacketDataType)}.{nameof(PacketDataType.Length)}");

        internal static SerializationException AttributeRequiredWithLength(string type) =>
            new(
                $"In {type} {nameof(PacketDataType)}.{nameof(PacketDataType.Length)} could not work without {nameof(PacketDataType)}.{nameof(PacketDataType.Body)} or {nameof(PacketDataType)}.{nameof(PacketDataType.Body)}");

        internal static SerializationException AttributeDuplicate(string type, string attributeType) =>
            new($"{type} could not work with multiple {attributeType}");

        internal static SerializationException AttributeInBody(string type, string attributeType) =>
            new($"{type} could not work with {attributeType} inside body");

        internal static SerializationException SerializerBodyPropertyIsNull() =>
            new($"Value of {nameof(PacketDataType)}.{nameof(PacketDataType.Body)} is Null");
    }
}