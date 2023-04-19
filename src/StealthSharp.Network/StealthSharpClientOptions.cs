#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientOptions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System.IO.Pipelines;
using System.Net.Sockets;

#endregion

namespace StealthSharp.Network
{
    public class StealthSharpClientOptions
    {
        /// <summary>
        ///     Represents a set of options for controlling the creation of the <see cref="PipeReader" /> for the
        ///     <see cref="NetworkStream" />.
        /// </summary>
        public StreamPipeReaderOptions StreamPipeReaderOptions { get; init; } = new();

        /// <summary>
        ///     Represents a set of options for controlling the creation of the <see cref="PipeWriter" /> for the
        ///     <see cref="NetworkStream" />.
        /// </summary>
        public StreamPipeWriterOptions StreamPipeWriterOptions { get; init; } = new();

        /// <summary>
        ///     Gets or sets the amount of time a <see cref="TcpClient" /> will wait for a send operation to complete successfully.
        /// </summary>
        public int TcpClientSendTimeout { get; init; }

        /// <summary>
        ///     Gets or sets the amount of time a <see cref="TcpClient" /> will wait to receive data once a read operation is
        ///     initiated.
        /// </summary>
        public int TcpClientReceiveTimeout { get; init; }

        /// <summary>
        ///     Gets default options
        ///     <returns>
        ///         <see cref="StealthSharpClientOptions" />
        ///     </returns>
        /// </summary>
        public static StealthSharpClientOptions Default => new()
        {
            StreamPipeReaderOptions = new StreamPipeReaderOptions(bufferSize: 65536),
            StreamPipeWriterOptions = new StreamPipeWriterOptions(),
            TcpClientSendTimeout = 60000,
            TcpClientReceiveTimeout = 60000
        };
    }
}