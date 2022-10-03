using AdventureGame.ObjectPooling;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.Effects {

    /// <summary>
    /// Send a event when the lifetime ends
    /// </summary>
    public class LifetimeEvent : Lifetime {

        [Header("Life Time Event")]
        [SerializeField] private UnityEvent onLifeTimeEnd;

        protected override void EndLifeTime() {
            onLifeTimeEnd.Invoke();
        }

        public void OnReturnToPool() {
            ObjectPool.ReturnToPool(this);
        }
    }
}