using System;
using System.Collections.Generic;

namespace Clock.Utils.EventBus
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _listeners = new Dictionary<Type, List<Delegate>>();

        public static void Subscribe<T>(Action<T> listener)
        {
            if (!_listeners.ContainsKey(typeof(T)))
            {
                _listeners[typeof(T)] = new List<Delegate>();
            }
            _listeners[typeof(T)].Add(listener);
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            if (_listeners.ContainsKey(typeof(T)))
            {
                _listeners[typeof(T)].Remove(listener);
            }
        }

        public static void Publish<T>(T eventData)
        {
            if (_listeners.ContainsKey(typeof(T)))
            {
                foreach (var listener in _listeners[typeof(T)])
                {
                    (listener as Action<T>)?.Invoke(eventData);
                }
            }
        }
    }
}