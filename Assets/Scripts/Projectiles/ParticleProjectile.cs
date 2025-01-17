﻿using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.Projectiles
{
    public class ParticleProjectile : Projectile
    {
        [Title("Particle Projectile")]
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private UnityEvent onImpact;

        private bool returningToPool;

        private void OnEnable()
        {
            particles.Play();
            rigidbody.detectCollisions = true;
            rigidbody.isKinematic = false;
            returningToPool = false;
        }

        public override void Impact()
        {
            ImpactVFX();

            particles.Stop();
            rigidbody.detectCollisions = false;
            rigidbody.isKinematic = true;
            returningToPool = true;

            onImpact.Invoke();
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
