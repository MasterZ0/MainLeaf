using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.Projectiles
{
    public class ParticleProjectile : Projectile
    {
        [Title("Particle Projectile")]
        [SerializeField] private ParticleSystem particles;

        private bool returningToPool;

        private void OnEnable()
        {
            particles.Play();
            rigidbod.detectCollisions = true;
            returningToPool = false;
        }

        public override void Impact()
        {
            ImpactVFX();

            particles.Stop();
            rigidbod.detectCollisions = false;
            returningToPool = true;
        }

        private void Update()
        {
            if (returningToPool && !particles.IsAlive(true))
            {
                ObjectPool.ReturnToPool(this);
            }
        }
    }
}
