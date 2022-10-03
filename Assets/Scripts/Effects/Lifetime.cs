using AdventureGame.ObjectPooling;
using System;
using UnityEngine;

namespace AdventureGame.Effects {

    /// <summary>
    /// Count the time and then remove the object
    /// </summary>
    public class Lifetime : MonoBehaviour {

        private float lifeTime = float.PositiveInfinity;
        public event Action OnLifetimeZero;
        public void SetLifeTime(float time) {
            lifeTime = time;
        }

        private void FixedUpdate() {

            lifeTime -= Time.fixedDeltaTime;
            if (lifeTime <= 0) {
                lifeTime = float.PositiveInfinity;
                LifetimeZeroCallback();
                EndLifeTime();
            }
        }
        protected void LifetimeZeroCallback()
        {
            if (OnLifetimeZero != null)
            {
                OnLifetimeZero();
                OnLifetimeZero = null;
            }
        }

        protected virtual void EndLifeTime() {
            LifetimeZeroCallback();
            ObjectPool.ReturnToPool(this);
        }
    }
}