﻿using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.ObjectPooling;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using AdventureGame.Effects;
using AdventureGame.Audio;
using System.Linq;
using AdventureGame.Gameplay.Components;
using Sirenix.Utilities;
using AdventureGame.Gameplay;

namespace AdventureGame.AI
{
    /// <summary>
    /// Defines enemy basic damage behaviour
    /// </summary>
    public class Enemy : MonoBehaviour, IStatusOwner
    {        
        [Title("Enemy")]
        [CustomBox]
        [SerializeField] private DataSettings<EnemyData> enemyData;
        [SerializeField] private Transform center;
        [SerializeField] private Transform head;

        [Header("Prefabs")]
        [SerializeField] private ParticleVFX hitFX;
        [SerializeField] private ParticleVFX hitKillFX;
        [SerializeField] private ParticleVFX deathFX;

        [Header("SFX")]
        [SerializeField] private SoundReference damageSoundReference;
        [SerializeField] private SoundReference deathSoundReference;


        [Header("Optional")]
        [SerializeField] private Renderer[] bodyRenderers;
        [ListDrawerSettings(Expanded = true)]
        [SerializeField] private HitBox[] bodyHitBoxes;

        #region Public properties and events
        public event Action OnFinishEnemyDeath = delegate { };

        public EnemyData EnemyData => enemyData;
        public Transform Center => center;
        public Transform Pivot => transform;
        public Transform Head => head;
        IStatusController IStatusOwner.Status => Status;
        #endregion

        public EnemyStatus Status { get; private set; }

        private Material[] defaultSharedMaterial;

        private bool dropItemEnabled;

        #region Initialization
        private void Awake()
        {
            defaultSharedMaterial = bodyRenderers.Select(r => r.sharedMaterial).ToArray();

            Status = new EnemyStatus(this);
        }

        private void OnEnable()
        {
            // Reset
            transform.localPosition = Vector2.zero;
            transform.localRotation = Quaternion.identity;
            dropItemEnabled = true;

            // Attributes and Status
            Status.Reset();

            // Body damage
            Damage bodyDamage = new Damage(EnemyData.BodyDamage, this);
            foreach (HitBox bodyHitBox in bodyHitBoxes)
            {
                bodyHitBox.SetDamage(bodyDamage);
                bodyHitBox.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Status
        private void FixedUpdate() => Status.Update();

        /// <summary> Damage VFX </summary>
        public void OnDamage(DamageInfo damageInfo)
        {
            HitVFX.ApplyHitFX(this, bodyRenderers, defaultSharedMaterial);
            damageSoundReference?.PlaySound(transform);

            if (!damageInfo.Damage.ShowHitParticle)
                return;

            GetContacts(damageInfo, out Vector3 position, out Quaternion rotation);

            // Paint Hit Particle
            ParticleVFX spawnedHitParticle = ObjectPool.SpawnPooledObject(hitFX, position, rotation);
            spawnedHitParticle.SetColor(EnemyData.HitParticleGradient);
        }

        /// <summary> Death VFX </summary>
        public void OnDeath(DamageInfo damageInfo)
        {
            GetContacts(damageInfo, out Vector3 position, out Quaternion rotation);

            deathSoundReference?.PlaySound(transform);
            ObjectPool.SpawnPooledObject(hitKillFX, position, rotation);

            // Disable components
            bodyHitBoxes.ForEach(bodyHitBox => bodyHitBox.gameObject.SetActive(false));
        }

        private void GetContacts(DamageInfo damageInfo, out Vector3 contactPoint, out Quaternion contactRotation)
        {
            contactPoint = damageInfo.Damage.ContactPoint ?? transform.position;
            contactRotation = damageInfo.Damage.ContactRotation ?? transform.rotation;
        }
        #endregion

        /// <summary> Disable the enemy after finish the death animation  </summary>
        public void FinishEnemyDeath()
        {
            ObjectPool.SpawnPooledObject(deathFX, transform.position, transform.rotation);

            OnFinishEnemyDeath.Invoke();
            gameObject.SetActive(false);

            if (dropItemEnabled)
            {
                GameplayPrefabs.DropItems(EnemyData.DropValueData, transform);
            }
        }

        public void ForceKill(bool disableDrop = false)
        {
            dropItemEnabled = !disableDrop;
            this.Kill();
        }
    }   
}