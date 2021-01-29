#region Copyright

// -----------------------------------------------------------------------
// <copyright file="WaiterInfo.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

using System;
using System.Threading.Tasks;

namespace StealthSharp.Network
{
    /// <summary>
    ///     Information about current waiter
    /// </summary>
    public class WaiterInfo<TResult>
    {
        /// <summary>
        ///     Current status of Task
        /// </summary>
        public TaskStatus TaskStatus { get; }

        /// <summary>
        ///     Result from Task (if IsCompletedSuccessfully)
        /// </summary>
        public TResult? Result { get; }

        /// <summary>
        ///     Exception from Task (if IsFaulted)
        /// </summary>
        public Exception? TaskException { get; }

        public WaiterInfo(Task<TResult> task)
        {
            TaskStatus = task.Status;

            if (task.IsCompletedSuccessfully)
                Result = task.Result;

            if (task.IsFaulted)
                TaskException = task.Exception;
        }
    }
}