using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _eventTable = new();

    public static void Subscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existing))
            _eventTable[type] = Delegate.Combine(existing, callback);
        else
            _eventTable[type] = callback;
    }

    public static void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var existing))
        {
            var updated = Delegate.Remove(existing, callback);
            if (updated == null)
                _eventTable.Remove(type);
            else
                _eventTable[type] = updated;
        }
    }

    public static void Publish<T>(T evt = default(T))
    {
        var type = typeof(T);
        if (_eventTable.TryGetValue(type, out var del) && del is Action<T> action)
            action.Invoke(evt);
    }
}
