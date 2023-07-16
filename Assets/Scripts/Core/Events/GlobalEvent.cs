using System;
using UnityEngine;

namespace Core.Events
{
    public abstract class ScriptableEvent<T> : ScriptableObject
    {
        [NonSerialized]
        private Action<T> action;

        public void Subscribe(Action<T> action)
        {
            this.action += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            this.action -= action;
        }

        public void Publish(T value)
        {
            action?.Invoke(value);
        }
    }
}
