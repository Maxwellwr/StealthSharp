#region Copyright

// -----------------------------------------------------------------------
// <copyright file="StealthSharpClientOptions.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System.IO.Pipelines;
using System.Net.Sockets;

namespace StealthSharp.Network
{
    public class StealthSharpClientOptions
    {
        /// <summary>
        ///     Represents a set of options for controlling the creation of the <see cref="PipeReader" /> for the
        ///     <see cref="NetworkStream" />.
        /// </summary>
        public StreamPipeReaderOptions StreamPipeReaderOptions { get; set; }

        /// <summary>
        ///     Represents a set of options for controlling the creation of the <see cref="PipeWriter" /> for the
        ///     <see cref="NetworkStream" />.
        /// </summary>
        public StreamPipeWriterOptions StreamPipeWriterOptions { get; set; }

        /// <summary>
        ///     Gets or sets the amount of time a <see cref="TcpClient" /> will wait for a send operation to complete successfully.
        /// </summary>
        public int TcpClientSendTimeout { get; set; }

        /// <summary>
        ///     Gets or sets the amount of time a <see cref="TcpClient" /> will wait to receive data once a read operation is
        ///     initiated.
        /// </summary>
        public int TcpClientReceiveTimeout { get; set; }

        /// <summary>
        ///     Gets or sets a value about sorting in reverse order for all byte arrays of primitive values.
        /// </summary>
        public bool PrimitiveValueReverse { get; set; }

        /// <summary>
        ///     Gets or sets a <see cref="TcpConverter" /> collection that will be used during serialization.
        /// </summary>
        // public IList<TcpConverter> Converters { get; set; }
        public StealthSharpClientOptions()
        {
            // Converters = new List<TcpConverter>();
        }

        /// <summary>
        ///     Gets default options
        ///     <returns>
        ///         <see cref="TcpClientIoOptions" />
        ///     </returns>
        /// </summary>
        public static StealthSharpClientOptions Default => new()
        {
            StreamPipeReaderOptions = new StreamPipeReaderOptions(bufferSize: 65536),
            StreamPipeWriterOptions = new StreamPipeWriterOptions(),
            TcpClientSendTimeout = 60000,
            TcpClientReceiveTimeout = 60000
        };

        /// <summary>
        /// Register converter
        /// </summary>
        /// <param name="tcpConverter"></param>
        // public StealthSharpClientOptions RegisterConverter(TcpConverter tcpConverter)
        // {
        //     Converters.Add(tcpConverter);
        //     return this;
        // }
    }
}