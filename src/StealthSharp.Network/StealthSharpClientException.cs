#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientException.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;

namespace StealthSharp.Network
{
    public class StealthSharpClientException : Exception
    {
        private StealthSharpClientException(string message) : base(message)
        {
        }

        public static StealthSharpClientException ConverterError(string converterName) =>
            new($"Converter {converterName} does not have generic type");

        public static StealthSharpClientException ConnectionBroken() => new("Connection was broken");
    }
}