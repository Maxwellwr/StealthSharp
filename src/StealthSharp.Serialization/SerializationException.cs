#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializationException.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Serialization
{
    internal class SerializationException : Exception
    {
        private SerializationException(string? message) : base(message)
        {
        }

        public static SerializationException CollectionItemTypeNotFoundException(string missingTypeName)
        {
            return new SerializationException($"Element type of collection {missingTypeName} not found");
        }

        public static SerializationException ConverterNotFoundType(string typeName)
        {
            return new SerializationException($"Converter not found for type {typeName}");
        }

        public static SerializationException SpanSizeException(string typeName)
        {
            return new SerializationException($"Array length lower, then size of type {typeName}");
        }

        public static SerializationException CreateInstanceException(string typeName)
        {
            return new SerializationException($"Can not create instance of type {typeName}");
        }

        public static SerializationException NullNotSupportedException(string message)
        {
            return new SerializationException("Null  not supported. " + message);
        }
    }
}