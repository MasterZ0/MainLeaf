using UnityEngine;
using System;

namespace AdventureGame.Shared
{
    public abstract class StringEvent : MonoBehaviour
    {
        public event Action<string> OnEventTrigger;

        protected void Invoke(string eventName) => OnEventTrigger?.Invoke(eventName);
    }
}