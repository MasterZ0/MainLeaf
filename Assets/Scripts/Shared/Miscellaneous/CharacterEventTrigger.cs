using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;
using Z3.UIBuilder.Core;

namespace AdventureGame.Shared
{
    public class CharacterEventTrigger : StringEvent
    {
        [Serializable]
        public struct EventReference
        {
            public string eventName;
            public string state;
            //[ShowIf("@!string.IsNullOrEmpty(state)")]
            public int layer;
            public UnityEvent unityEvent;
        }

        [Title("Event Trigger")]
        [SerializeField] private Animator animator;
        [SerializeField] private EventReference[] eventReferences;

        public void OnEvent(string eventName)
        {
            EventReference reference = eventReferences.First(e => e.eventName == eventName);

            if (!string.IsNullOrEmpty(reference.state) && animator.IsInTransition(reference.layer))
                return;

            reference.unityEvent.Invoke();
            Invoke(eventName);
        }
    }
}