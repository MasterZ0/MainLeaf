using AdventureGame.ObjectPooling;
using UnityEngine;
using Sirenix.OdinInspector;

namespace AdventureGame.Effects
{
    /// <summary>
    /// Return object when effect ends
    /// </summary>
    public class AnimationVFX : MonoBehaviour {

        [Title("Animation VFX")]
        [SerializeField] protected Animator animator;

        private float lifeTime = float.PositiveInfinity;

        private void Reset() => TryGetComponent(out animator);

        protected virtual void OnEnable()
        {
            lifeTime = animator.GetCurrentAnimatorStateInfo(0).length;
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                this.ReturnToPool();
            }
        }
    }
}