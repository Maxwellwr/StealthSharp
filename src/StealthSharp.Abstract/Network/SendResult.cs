#region Copyright
// // -----------------------------------------------------------------------
// // <copyright file="SendResult.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------
#endregion

namespace StealthSharp.Network
{
    public struct SendResult<TId>
    {
        public bool Result;
        public TId CorrelationId;
    }
}