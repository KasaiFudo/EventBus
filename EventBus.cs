using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MainAssets._Scripts.Gameplay.Cards;

namespace MainAssets._Scripts.Utils
{
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
    
    //Events
    
    public struct OnGameClear { }
    public struct OnCardClick
    {
        public Card ClickedCard { get;  set; }
    }
    
    public struct OnDragCard
    {
        public bool IsBeginDrug { get;  set; }
    }
    
    public struct OnEndDragCard
    {
        public Card DraggedCard { get;  set; }
        [CanBeNull] public Card TargetCard { get;  set; }
    }
}
