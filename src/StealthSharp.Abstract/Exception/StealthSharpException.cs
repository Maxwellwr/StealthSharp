#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientException.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using StealthSharp.Enumeration;

#endregion

namespace StealthSharp
{
    public class StealthSharpException : Exception
    {
        private StealthSharpException(string message) : base(message)
        {
        }

        public static StealthSharpException ConverterError(string converterName)
        {
            return new StealthSharpException($"Converter {converterName} does not have generic type");
        }

        public static StealthSharpException ConnectionBroken()
        {
            return new StealthSharpException("Connection was broken");
        }

        public static StealthSharpException StealthError(ErrorCode errorCode)
        {
            return new StealthSharpException($"Error code {errorCode}");
        }

        public static StealthSharpException EventNotFound(EventType eventType)
        {
            return new StealthSharpException($"Event {eventType} not found in Enum {nameof(EventType)}");
        }

        public static StealthSharpException EventNotSupportedData(EventType eventType, Type eventDataType)
        {
            return new StealthSharpException($"Not supported {eventDataType} type for event {eventType}");
        }

        public static StealthSharpException ErrorCreateType(Type type)
        {
            return new StealthSharpException($"Create instance of event type {type} failed");
        }

        public static StealthSharpException UnknownParameterType(Type propertyType)
        {
            return new StealthSharpException($"Type {propertyType} can't be used in event data");
        }

        public static StealthSharpException UnknownParameterType(ParameterType paramType)
        {
            return new StealthSharpException($"Unsupported parameter {paramType}");
        }
    }
}