using AdventureGame.ObjectPooling;
using UnityEngine;
using Z3.UIBuilder.Core;

namespace AdventureGame.Effects {

    /// <summary>
    /// Spawn HideEffect with a frequency defined
    /// </summary>
    public class TrailGenerator : FrequencyCall {

        [Title("Trail Generator")]
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Prefab")]
        [SerializeField] private HideEffect prefab;

        protected float effectDuration = -1;

        private void Reset() => TryGetComponent(out spriteRenderer);

        public void Init(float trailFrequency, float effectDuration) {
            this.spawnFrequency = trailFrequency;
            this.effectDuration = effectDuration;
        }

        public override void Spawn() {
            ObjectPool.SpawnPooledObject(prefab, transform.position, transform.rotation).Init(effectDuration, spriteRenderer);
        }
    }
}