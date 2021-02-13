#region Copyright

// -----------------------------------------------------------------------
// <copyright file="SerializationException.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Serialization
{
    internal class SerializationException : Exception
    {
        private SerializationException(string? message) : base(message)
        {
        }

        public static SerializationException CollectionItemTypeNotFoundException(string missingTypeName) =>
            new ($"Element type of collection {missingTypeName} not found");

        public static SerializationException ConverterNotFoundType(string typeName) =>
            new ($"Converter not found for type {typeName}");

        public static SerializationException SpanSizeException(string typeName) =>
            new($"Array length lower, then size of type {typeName}");

        public static SerializationException CreateInstanceException(string typeName) =>
            new($"Can not create instance of type {typeName}");
        
        public static SerializationException NullNotSupportedException(string message) =>
            new("Null  not supported. " + message);
    }
}