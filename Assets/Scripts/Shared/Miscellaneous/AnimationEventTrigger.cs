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
    public class AnimationEventTrigger : MonoBehaviour 
    {        
        [Serializable]
        public struct EventReference
        {
            public string eventName;
            public UnityEvent unityEvent;
        }

        [Title("Event Trigger")]
        [SerializeField] private EventReference[] eventReferences;

        public event Action<string> OnEventTrigger;

        public void OnEvent(string eventName)
        {
            EventReference reference = eventReferences.First(k => k.eventName == eventName);
            reference.unityEvent.Invoke();
            OnEventTrigger?.Invoke(eventName);
        }
    }
}