using AdventureGame.ObjectPooling;
using UnityEngine;

namespace AdventureGame.Effects {

    /// <summary>
    /// Hide the SpriteRenderer gradually
    /// </summary>
    public class HideEffect : MonoBehaviour {

        [Header("Hide Effect")]
        [SerializeField] protected SpriteRenderer spriteRenderer;

        protected Color color;
        protected float effectDuration;

        protected virtual void Awake() {
            color = spriteRenderer.color;
        }

        public void Init(float effectDuration) {
            color.a = 1;
            this.effectDuration = effectDuration;
        }

        public void Init(float effectDuration, Sprite sprite) {
            color.a = 1;
            this.effectDuration = effectDuration;
            spriteRenderer.sprite = sprite;
        }

        /// <summary>
        /// Generic prefab
        /// </summary>
        public void Init(float effectDuration, SpriteRenderer copy) {
            color.a = 1;
            this.effectDuration = effectDuration;
            spriteRenderer.sprite = copy.sprite;
            spriteRenderer.flipX = copy.flipX;
            spriteRenderer.flipY = copy.flipY;
        }

        protected virtual void FixedUpdate() {
            color.a -= Time.fixedDeltaTime / effectDuration;
            spriteRenderer.color = color;

            if (color.a <= 0) {
                ObjectPool.ReturnToPool(this);
            }
        }
    }
}