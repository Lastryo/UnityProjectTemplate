using System;
using System.Collections.Generic;
namespace Core.Events
{
    internal class EventHolder
    {
        internal readonly Dictionary<Type, EventBase> RegisteredEvents = new Dictionary<Type, EventBase>();
        internal List<EventBase> DispatchedEvents = new List<EventBase>();
        internal List<EventBase> ExecutingEvents = new List<EventBase>();
    }
}
