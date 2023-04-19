#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketCorrelationGenerator.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using StealthSharp.Network;

#endregion

namespace StealthSharp
{
    public class PacketCorrelationGenerator : IPacketCorrelationGenerator<ushort>
    {
        private ushort _nextId;

        public ushort GetNextCorrelationId()
        {
            if (_nextId == ushort.MaxValue)
                _nextId = 0;
            return ++_nextId;
        }
    }
}