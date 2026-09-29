#region Copyright

// -----------------------------------------------------------------------
// <copyright file="PacketCorrelationGenerator.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.Threading;
using StealthSharp.Network;

#endregion

namespace StealthSharp
{
    public class PacketCorrelationGenerator : IPacketCorrelationGenerator<ushort>
    {
        private int _nextId;

        /// <summary>Thread safe. Ids run 1..65535 and then wrap to 1; 0 is never used.</summary>
        public ushort GetNextCorrelationId()
        {
            while (true)
            {
                var current = Volatile.Read(ref _nextId);
                var next = current >= ushort.MaxValue ? 1 : current + 1;
                if (Interlocked.CompareExchange(ref _nextId, next, current) == current)
                    return (ushort)next;
            }
        }
    }
}