using AdventureGame.Shared;
using AdventureGame.Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace AdventureGame.BattleSystem
{
    public class Damage
    {
        // Damage
        public int Value { get; private set; }
        public int StaminaDamage { get; } // Only blockable damages
        // public int Intensity { get; } // Injury?
        public bool CanBlock { get; }
        public DamageType DamageType { get; }
        public IAttacker Sender { get; }
        public List<HitEffect> HitEffects { get; }

        // Hitbox
        public HitBox HitBoxSender { get; private set; }
        public Vector2 ContactPoint { get; private set; }

        public Damage(DamageData damageData, IAttacker sender)
        {
            StaminaDamage = damageData.Value.x;
            Value = damageData.Value.RandomRange();

            CanBlock = damageData.CanBlock;
            DamageType = damageData.DamageType;
            //HitEffects = damageData.HitEffects;
            Sender = sender;
        }

        public Damage(int value)
        {
            Value = value;
        }

        public void AddHitBoxInfo(HitBox hitBox, Vector2 contact)
        {
            HitBoxSender = hitBox;
            ContactPoint = contact;
        }

        /// <summary> Get Knockback direction </summary>
        public float GetXDirection(Transform transform)
        {
            float targetX = HitBoxSender.transform.position.x - transform.position.x;
            return targetX > 0 ? 1 : -1;
        }
        
        public bool HasEffect<T>() where T : HitEffect
        {
            return HitEffects.OfType<T>().Any();
        }
    }
}