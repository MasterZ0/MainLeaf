using AdventureGame.Shared;
using AdventureGame.Shared.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace AdventureGame.BattleSystem
{
    public class Damage
    {
        public Transform HitBoxSender { get; private set; }
        public Vector2 ContactPoint { get; private set; }
        public int Value { get; private set; }
        public int BlockingDamage { get; }
        public bool Critical { get; }
        public bool CanBlock { get; }
        public DamageType DamageType { get; }
        public Transform Sender { get; }
        public string SenderName { get; }
        public List<HitEffect> HitEffects { get; }

        private const string UNKNOWN_SENDER_NAME = "Unknown";

        private Damage(DamageData damageData, Transform sender)
        {
            CanBlock = damageData.CanBlock;
            DamageType = damageData.DamageType;
            HitEffects = damageData.HitEffects;
            Sender = sender;
            SenderName = Sender != null ? Sender.name : UNKNOWN_SENDER_NAME;
        }
        
        public Damage(DamageData damageData) : this (damageData, null)
        {
            BlockingDamage = damageData.Value.y;
            Value = damageData.Value.RandomRange();
        }

        /// <summary> Player Only </summary>
        public Damage(DamageData damageData, Transform sender, int damage, bool critical) : this (damageData, sender)
        {
            BlockingDamage = damageData.Value.y;
            Critical = critical;
            Value = damage;
        }

        /// <summary> Aberration receving monster death damage </summary>
        /// <remarks> MasterZ: There is no associated HitBox or Contact, you can change this method if necessary </remarks>
        public Damage(Transform sender, int damageValue)
        {
            Sender = sender;
            SenderName = Sender != null ? Sender.name : UNKNOWN_SENDER_NAME;
            Value = damageValue;
            HitEffects = new List<HitEffect>();
            DamageType = DamageType.Default;
        }
        
        /// <summary> Aberration </summary>
        public void SetDamage(int damage) => Value = damage;
        
        public void AddHitBoxInfo(Transform hitboxSender, Vector2 contactPoint)
        {
            HitBoxSender = hitboxSender;
            ContactPoint = contactPoint;
        }

        /// <summary> Get Knockback direction </summary>
        public float GetXDirection(Transform transform)
        {
            float targetX = HitBoxSender.position.x - transform.position.x;
            return targetX > 0 ? 1 : -1;
        }
        
        public bool HasEffect<T>() where T : HitEffect
        {
            return HitEffects.OfType<T>().Any();
        }
    }
}