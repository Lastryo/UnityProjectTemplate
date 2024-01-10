using System;
using System.Collections.Generic;

namespace Core.Events
{
    public class Event<T> : EventBase where T : struct, IEventData
    {
        public readonly List<T> publishedChanges = new List<T>();
        public readonly List<T> scheduledChanges = new List<T>();
        public bool isPublished;
        public bool isScheduled;
        internal event Action<List<T>> Callback;

        public List<T> BatchedChanges => publishedChanges;
        public List<T> ScheduledChanges => scheduledChanges;
        public bool IsPublished => isPublished;
        public bool IsScheduled => isScheduled;

        internal override void Invoke()
        {
            if (isPublished)
            {
                if (Callback != null)
                    ForwardInvokeCallback();

                isPublished = false;
                publishedChanges.Clear();
            }

            if (isScheduled)
            {
                isPublished = true;
                isScheduled = false;
                publishedChanges.AddRange(scheduledChanges);
                scheduledChanges.Clear();

                holder.DispatchedEvents.Add(this);
            }
        }

        private void ForwardInvokeCallback()
        {
            Callback?.Invoke(publishedChanges);
        }

        public void Send(T data)
        {
            scheduledChanges.Add(data);

            if (!isPublished && !isScheduled)
            {
                holder.DispatchedEvents.Add(this);
            }

            isScheduled = true;
        }

        public IDisposable Subscribe(Action<List<T>> callback)
        {
            return new Subscription(this, callback);
        }

        internal class Subscription : IDisposable
        {
            private readonly Event<T> _owner;
            private readonly Action<List<T>> _callback;

            public Subscription(Event<T> owner, Action<List<T>> callback)
            {
                _owner = owner;
                _callback = callback;

                _owner.Callback += _callback;
            }

            public void Dispose()
            {
                _owner.Callback -= _callback;
            }
        }
    }

    public abstract class EventBase
    {
        internal EventHolder holder;

        internal abstract void Invoke();
    }
}