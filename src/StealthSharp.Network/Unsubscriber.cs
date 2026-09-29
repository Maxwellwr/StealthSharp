#region Copyright

// // -----------------------------------------------------------------------
// // <copyright file="Unsubscriber.cs" company="StealthSharp">
// // Copyright (c) StealthSharp. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.
// // </copyright>
// // -----------------------------------------------------------------------

#endregion

#region

using System;

#endregion

namespace StealthSharp.Network
{
    internal class Unsubscriber<T> : IDisposable
    {
        private readonly Action<IObserver<T>> _unsubscribe;
        private readonly IObserver<T> _observer;

        internal Unsubscriber(Action<IObserver<T>> unsubscribe, IObserver<T> observer)
        {
            _unsubscribe = unsubscribe;
            _observer = observer;
        }

        public void Dispose()
        {
            _unsubscribe(_observer);
        }
    }
}