using System;
using AdventureGame.BattleSystem;
using AdventureGame.Effects;
using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace AdventureGame.AI.Entities 
{
    /// <summary>
    /// Implements basic behaviour for hittable objects
    /// </summary>
    public class Hittable : MonoBehaviour, IHittable 
    {
        [Title("Hittable")]
        [SerializeField] private bool immortal;
        [SerializeField] private Transform center;

        [Title("Effects")] 
        [SerializeField] private ParticleVFX hitParticles;
        [SerializeField] private Gradient hitParticlesColor;
        
        [Title("Events")]
        [SerializeField] private UnityEvent unityEvent;

        public event Action<DamageInfo> OnTakeDamage = delegate {  };

        public Transform Center => center;
        public Transform Pivot => transform;

        public void TakeDamage(Damage damage)
        {
            unityEvent.Invoke();

            if (hitParticles)
            {
                ParticleVFX hitParticleInstance = ObjectPool.SpawnPooledObject(hitParticles, transform.position);
                hitParticleInstance.SetColor(hitParticlesColor);
            }

            if (!immortal)
            {
                gameObject.SetActive(false);
            }
        }
    }
}