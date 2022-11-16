using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System.Linq;
using System;

namespace AdventureGame.Shared
{
    /// <summary>
    /// Easy to call a Unity Event
    /// </summary>
    public class AnimationEventTrigger : StringEvent
    {
        [Serializable]
        public struct EventReference
        {
            public string eventName;
            public UnityEvent unityEvent;
        }

        [Title("Event Trigger")]
        [SerializeField] private EventReference[] eventReferences;

        public void OnEvent(string eventName)
        {
            EventReference reference = eventReferences.First(e => e.eventName == eventName);
            reference.unityEvent.Invoke();
            Invoke(eventName);
        }
    }
}