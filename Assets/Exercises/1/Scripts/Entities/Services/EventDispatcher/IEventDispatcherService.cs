using System;

namespace UnityExercises.Entities.Services.EventDispatcher
{
    public interface IEventDispatcherService
    {
        void Subscribe<T>(Action<T> callback);
        void Unsubscribe<T>(Action<T> callback);
        void Dispatch<T>(T arg = default);
    }
}