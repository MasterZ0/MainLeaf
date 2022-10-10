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
using UnityEngine.UI;

namespace AdventureGame.AI
{
    /// <summary>
    /// Defines enemy basic damage behaviour
    /// </summary>
    public class Enemy : MonoBehaviour, IAttacker, IDamageable
    {        
        [Title("Enemy")]
        [SerializeField] private Transform center;
        [SerializeField] private Slider healthBar;
        [SerializeField] private ParticleVFX hitParticle;

        [Title("Optional")]
        [ListDrawerSettings(Expanded = true)]
        [SerializeField] private HitBox[] bodyHitBoxes;

        [Title("SFX")]
        [SerializeField] private SoundReference damageSoundReference;
        [SerializeField] private SoundReference deathSoundReference;

        #region Public properties and events
        public event Action<DamageInfo> OnTakeDamage = delegate { };
        public Transform Center => center;
        public Transform Pivot => transform;
        public float HealthPercentage => (float)currentHealth / maxHealth;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsDead => currentHealth <= 0;
        #endregion

        private readonly List<SkillEffect> modifiers = new List<SkillEffect>();

        private EnemyData enemyData;

        private int maxHealth = 1;
        private int currentHealth = 1;

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

            //DamageData bodyDamage = enemyData.BodyDamage.Setup(transform);
            //foreach (HitBox bodyHitBox in bodyHitBoxes)
            //{
            //    bodyHitBox.SetDamage(bodyDamage);
            //    bodyHitBox.gameObject.SetActive(true);
            //}

            RefreshLife();
        }

        #region Damage
        public void TakeDamage(Damage damage)
        {
            if (IsDead)
                return;

            currentHealth -= damage.Value;
            damageSoundReference.PlaySound(transform);
            RefreshLife();


            // Paint Hit Particle
            ParticleVFX spawnedHitParticle = ObjectPool.SpawnPooledObject(hitParticle, center.position, damage.HitBoxSender.transform.rotation);
            spawnedHitParticle.SetColor(enemyData.HitParticleGradient);

            DamageInfo damageInfo = null;
            damage.Sender.OnDamageDealt(damageInfo);
            OnTakeDamage(damageInfo);

            if (IsDead)
            {
                deathSoundReference.PlaySound(transform);

                // Disable components
                healthBar.gameObject.SetActive(false);
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
 
        public void OnDamageDealt(DamageInfo damageInfo) { }

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

        private void RefreshLife()
        {
            healthBar.value = HealthPercentage;
        }

        public void KillEnemy()
        {
            gameObject.SetActive(false);
            //GameplayController.GameplayReferences.SpawnItems(enemyData.DropValueData, transform.position);
        }
        #endregion
    }
}
