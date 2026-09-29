#region Copyright

// -----------------------------------------------------------------------
// <copyright file="MockRequest.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using StealthSharp.Enumeration;

#endregion

namespace StealthSharp.MockServer
{
    /// <summary>
    ///     A request received by <see cref="MockStealthServer" />.
    /// </summary>
    /// <param name="PacketType">Packet type from the packet header.</param>
    /// <param name="CorrelationId">Id the response has to be sent with.</param>
    /// <param name="Body">Raw serialized body (everything after the correlation id).</param>
    public readonly record struct MockRequest(PacketType PacketType, ushort CorrelationId, ReadOnlyMemory<byte> Body);
}
