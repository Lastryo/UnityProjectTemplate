using System;
namespace Core.Events
{
    public static class EventExtensions
    {
        public static Event<T> GetEvent<T>()
                    where T : struct, IEventData
        {
            var type = typeof(T);
            var holder = EventManager.Instance.EventHolder;

            if (holder.RegisteredEvents.TryGetValue(type, out var registeredEvent))
            {
                return (Event<T>)registeredEvent;
            }

            registeredEvent = new Event<T>
            {
                holder = holder
            };

            holder.RegisteredEvents.Add(type, registeredEvent);

            return (Event<T>)registeredEvent;
        }

        public static EventBase GetReflectionEvent(Type type)
        {
            var holder = EventManager.Instance.EventHolder;

            if (holder.RegisteredEvents.TryGetValue(type, out var registeredEvent))
            {
                return registeredEvent;
            }

            var constructedType = typeof(Event<>).MakeGenericType(type);
            registeredEvent = (EventBase)Activator.CreateInstance(constructedType, true);
            registeredEvent.holder = holder;

            holder.RegisteredEvents.Add(type, registeredEvent);

            return registeredEvent;
        }
    }
}
