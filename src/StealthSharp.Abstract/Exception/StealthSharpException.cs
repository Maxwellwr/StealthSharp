#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientException.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using StealthSharp.Enumeration;

namespace StealthSharp
{
    public class StealthSharpException : Exception
    {
        private StealthSharpException(string message) : base(message)
        {
        }

        public static StealthSharpException ConverterError(string converterName) =>
            new($"Converter {converterName} does not have generic type");

        public static StealthSharpException ConnectionBroken() => new("Connection was broken");
        
        public static StealthSharpException StealthError(ErrorCode errorCode) => new($"Error code {errorCode}");

        public static StealthSharpException EventNotFound(EventType eventType) =>
            new($"Event {eventType} not found in Enum {nameof(EventType)}");

        public static StealthSharpException EventNotSupportedData(EventType eventType, Type eventDataType) =>
            new($"Not supported {eventDataType} type for event {eventType}");

        public static StealthSharpException ErrorCreateType(Type type) => 
            new($"Create instance of event type {type} failed");

        public static StealthSharpException UnknownParameterType(Type propertyType) => 
            new($"Type {propertyType} can't be used in event data");

        public static StealthSharpException UnknownParameterType(ParameterType paramType) => 
            new($"Unsupported parameter {paramType}");
    }
}