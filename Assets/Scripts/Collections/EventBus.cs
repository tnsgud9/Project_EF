using System.Collections.Generic;
using UnityEngine.Events;

namespace Collections
{
    public interface IEventBus<T>
    {
        public static void Subscribe(T eventType, UnityAction listener)
        {
        }

        public static void Unsubscribe(T type, UnityAction listener)
        {
        }

        public static void Publish(T type)
        {
        }
    }

    public class EventBus<T> : IEventBus<T>
    {
        private static readonly IDictionary<T, UnityEvent> Events = new Dictionary<T, UnityEvent>();

        public static void Subscribe(T eventType, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(eventType, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Events.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(T type, UnityAction listener)
        {
            UnityEvent thisEvent;

            if (Events.TryGetValue(type, out thisEvent)) thisEvent.RemoveListener(listener);
        }

        public static void Publish(T type)
        {
            UnityEvent thisEvent;
            if (Events.TryGetValue(type, out thisEvent)) thisEvent.Invoke();
        }
    }
}