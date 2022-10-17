using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureGame.Effects
{
    /// <summary>
    /// Set multiple particle system's duration and return to pool when finish.
    /// </summary>
    public class MultipleParticlesVFX : ParticleVFX
    {
        [SerializeField] private List<ParticleSystem> extraParticles;

        protected override void Reset()
        {
            extraParticles = GetComponentsInChildren<ParticleSystem>(true).ToList();

            if (extraParticles.Count > 0)
            {
                particles = extraParticles[0];
                extraParticles.Remove(particles);
            }
        }

        public override void SetColor(Gradient color)
        {
            base.SetColor(color); 

            foreach (ParticleSystem particleSystem in extraParticles)
            {
                ParticleSystem.MainModule othersMainModule = particleSystem.main;
                othersMainModule.startColor = new ParticleSystem.MinMaxGradient(color);
            }
        }

        public override void SetupParticles(float duration)
        {
            base.SetupParticles(duration);

            foreach (ParticleSystem extraParticle in extraParticles)
            {
                extraParticle.Stop();
                ParticleSystem.MainModule main = extraParticle.main;
                main.duration = duration;
                extraParticle.Play();
            }
        }
    }
}