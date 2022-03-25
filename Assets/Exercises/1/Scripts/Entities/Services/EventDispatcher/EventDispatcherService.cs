using System;
using System.Collections.Generic;

namespace UnityExercises.Entities.Services.EventDispatcher
{
    public class EventDispatcherService : IEventDispatcherService
    {
        private readonly Dictionary<Type, dynamic> _events = new Dictionary<Type, dynamic>();

        public void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (!_events.ContainsKey(type))
            {
                _events.Add(type, null);
            }

            _events[type] += callback;
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (_events.ContainsKey(type))
            {
                _events[type] -= callback;
            }
        }

        public void Dispatch<T>(T arg = default)
        {
            var type = typeof(T);
            if (_events.ContainsKey(type))
            {
                _events[type](arg);
            }
        }
    }
}