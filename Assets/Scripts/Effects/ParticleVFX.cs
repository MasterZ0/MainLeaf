using AdventureGame.ObjectPooling;
using System;
using UnityEngine;

namespace AdventureGame.Effects 
{
    /// <summary>
    /// Set the particle duration and return to pool when finish.
    /// </summary>
    public class ParticleVFX : MonoBehaviour 
    {
        [SerializeField] protected ParticleSystem particles;

        protected void Reset() => TryGetComponent(out particles);

        public void SetColor(Gradient color)
        {
            ParticleSystem.MainModule hitParticleMain = particles.main;
            hitParticleMain.startColor = new ParticleSystem.MinMaxGradient(color);
        }

        public virtual void SetupParticles(float duration) 
        {
            particles.Stop();
            ParticleSystem.MainModule main = particles.main;
            main.duration = duration;
            particles.Play();
        }

        private void Update()
        {
            if (!particles.IsAlive(true))
            {
                ObjectPool.ReturnToPool(this);
            }
        }
    }
}