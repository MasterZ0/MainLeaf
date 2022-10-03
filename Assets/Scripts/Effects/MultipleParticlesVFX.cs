using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame.Effects
{
    /// <summary>
    /// Set multiple particle system's duration and return to pool when finish.
    /// </summary>
    public class MultipleParticlesVFX : ParticleVFX
    {
        [SerializeField] private List<ParticleSystem> extraParticles;
        public override void SetupParticles(float duration)
        {
            base.SetupParticles(duration);
            ParticleSystem.MainModule main;
            foreach (ParticleSystem extraParticle in extraParticles)
            {
                extraParticle.Stop();
                main = extraParticle.main;
                main.duration = duration;
                extraParticle.Play();
            }
        }
    }
}