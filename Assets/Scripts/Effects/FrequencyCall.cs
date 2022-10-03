using UnityEngine;
using System;

namespace AdventureGame.Effects {

    /// <summary>
    /// Spawn objects with a frequency defined
    /// </summary>
    public abstract class FrequencyCall : MonoBehaviour {

        protected float spawnFrequency = -1;
        private float time;

        protected virtual void OnEnable() => time = 0;

        protected virtual void FixedUpdate() 
        {
            if (spawnFrequency <= 0)
                return;

            time += Time.fixedDeltaTime;

            if (time >= spawnFrequency) {
                time = 0;

                Spawn();
            }
        }

        public abstract void Spawn();
    }
}