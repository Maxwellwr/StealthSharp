#region Copyright

// -----------------------------------------------------------------------
// <copyright file="EventSystemService.cs" company="StealthSharp">
// Copyright (c) StealthSharp. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#endregion

#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StealthSharp.Enumeration;
using StealthSharp.Event;
using StealthSharp.Network;

#endregion

namespace StealthSharp.Services
{
    public sealed class EventSystemService : BaseService, IEventSystemService, IObserver<ServerEventData>, IDisposable

    {
        private readonly Dictionary<EventType, MulticastDelegate?> _delegates = new();
        private readonly IDisposable _unsubscriber;

        public EventSystemService(IStealthSharpClient client)
            : base(client)
        {
            _unsubscriber = client.Subscribe(this);
        }

        public async Task Subscribe<T>(EventType eventType, Action<T> action)
        {
            if (typeof(T) != eventType.GetEnumDataType())
                throw StealthSharpException.EventNotSupportedData(eventType, typeof(T));

            if (!_delegates.ContainsKey(eventType))
            {
                await Client.SendPacketAsync(PacketType.SCSetEventProc, eventType).ConfigureAwait(false);
                _delegates[eventType] = null;
            }

            _delegates[eventType] = (MulticastDelegate?)Delegate.Combine(_delegates[eventType], action);
        }

        public async Task Unsubscribe(EventType eventType, Delegate action)
        {
            _delegates[eventType] = (MulticastDelegate?)Delegate.Remove(_delegates[eventType], action);
            if (_delegates.ContainsKey(eventType) && (_delegates[eventType] == null ||
                                                      _delegates[eventType]!.GetInvocationList().Length == 0))
            {
                await Client.SendPacketAsync(PacketType.SCClearEventProc, eventType).ConfigureAwait(false);
                _delegates.Remove(eventType);
            }
        }

        private void ProcessEvent(ServerEventData data)
        {
            if (_delegates.ContainsKey(data.EventType))
                _delegates[data.EventType]?.DynamicInvoke(data.GetEventData());
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ServerEventData value)
        {
            ProcessEvent(value);
        }

        public void Dispose()
        {
            _unsubscriber.Dispose();
        }
    }
}