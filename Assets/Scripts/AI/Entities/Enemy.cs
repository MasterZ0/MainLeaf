using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using AdventureGame.Effects;
using AdventureGame.Audio;
using System.Collections.Generic;
using System.Linq;
using AdventureGame.Gameplay.Components;

namespace AdventureGame.AI
{
    /// <summary>
    /// Defines enemy basic damage behaviour
    /// </summary>
    public class Enemy : MonoBehaviour, IAttacker, IDamageable
    {        
        [Title("Enemy")]
        [SerializeField] private Transform center;
        [SerializeField] private ParticleVFX hitFX;
        [SerializeField] private ParticleVFX hitKillFX;
        [SerializeField] private ParticleVFX deathFX;
        [SerializeField] private Renderer[] bodyRenderers;

        [Title("Optional")]
        [ListDrawerSettings(Expanded = true)]
        [SerializeField] private HitBox[] bodyHitBoxes;

        [Title("SFX")]
        [SerializeField] private SoundReference damageSoundReference;
        [SerializeField] private SoundReference deathSoundReference;

        #region Public properties and events
        public event Action<DamageInfo> OnTakeDamage = delegate { };
        public event Action OnFinishEnemyDeath = delegate { };

        public Transform Center => center;
        public Transform Pivot => transform;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsDead => currentHealth <= 0;
        #endregion

        private readonly List<SkillEffect> modifiers = new List<SkillEffect>();

        private EnemyData enemyData;

        private int maxHealth = 1;
        private int currentHealth = 1;

        private Material[] defaultSharedMaterial;

        private void Awake()
        {
            defaultSharedMaterial = bodyRenderers.Select(r => r.sharedMaterial).ToArray();
        }

        private void OnEnable() // Reset
        {
            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.identity;
        }

        /// <summary>
        /// Node Canvas Setup
        /// </summary>
        public virtual void Setup(EnemyData data)
        {
            enemyData = data;
            maxHealth = enemyData.MaxHealth;
            currentHealth = maxHealth;

            Damage bodyDamage = new Damage(enemyData.BodyDamage, this); 
            foreach (HitBox bodyHitBox in bodyHitBoxes)
            {
                bodyHitBox.SetDamage(bodyDamage);
                bodyHitBox.gameObject.SetActive(true);
            }
        }

        #region Combat
        public void OnDamageDealt(DamageInfo damageInfo) { }

        public void TakeDamage(Damage damage)
        {
            if (IsDead)
                return;

            int effectiveDamage = damage.Value;
            if (currentHealth - effectiveDamage < 0)
            {
                effectiveDamage = currentHealth;
            }

            currentHealth -= effectiveDamage;

            DamageInfo damageInfo = new DamageInfo(damage, this, effectiveDamage);
            OnTakeDamage(damageInfo);

            if (!IsDead)
            {
                HitVFX.ApplyHitFX(this, bodyRenderers, defaultSharedMaterial);
                damageSoundReference.PlaySound(transform);

                // Paint Hit Particle
                ParticleVFX spawnedHitParticle = ObjectPool.SpawnPooledObject(hitFX, damage.ContactPoint, damage.HitBoxSender.transform.rotation);
                spawnedHitParticle.SetColor(enemyData.HitParticleGradient);
            }
            else
            {
                deathSoundReference.PlaySound(transform);
                ObjectPool.SpawnPooledObject(hitKillFX, damage.ContactPoint, damage.HitBoxSender.transform.rotation);

                // Disable components
                foreach (HitBox bodyHitBox in bodyHitBoxes)
                {
                    bodyHitBox.gameObject.SetActive(false);
                }

                // Reset item effects
                foreach (HitEffect modifier in modifiers.ToList())
                {
                    modifiers.Remove(modifier);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!IsDead)
            {
                // Update Effects
                foreach (HitEffect modifier in modifiers.ToList()) // Clone list
                {
                    bool finish = modifier.Update();
                    if (finish)
                    {
                        modifier.Dispose();
                        modifiers.Remove(modifier);
                    }
                }
            }
        }

        public void FinishEnemyDeath()
        {
            ObjectPool.SpawnPooledObject(deathFX, transform.position, transform.rotation);
            //GameplayController.GameplayReferences.SpawnItems(enemyData.DropValueData, transform.position);

            OnFinishEnemyDeath.Invoke();
            gameObject.SetActive(false);
        }
        #endregion
    }
}
