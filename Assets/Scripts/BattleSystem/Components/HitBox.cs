using System;
using UnityEngine;

namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// Applies damage to an IDamageable when triggered
    /// </summary>
    public class HitBox : MonoBehaviour
    {
        protected enum TargetHitType
        {
            Unknown,
            Hittable,
            Damageable,
            KilledDamageable
        }

        public event Action<IHittable> OnApplyDamage;

        protected DamageData DamageData { get; private set; }
        protected Func<Damage> createDamage;

        public void SetDamage(DamageData damage, bool isFixedDamage = false)
        {
            DamageData = damage;
            if (isFixedDamage)
            {
                Damage dam = new Damage(damage);
                createDamage = () => dam;
            }
            else
            {
                createDamage = () => new Damage(damage);
            }
        }

        public void SetDamage(Damage damage)
        {
            createDamage = () => damage;
        }

        /// <summary> Player Only </summary>
        public void SetDamageGetter(Func<Damage> playerDamage)
        {
            createDamage = playerDamage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            ApplyDamage(collision);
        }

        protected virtual void OnParticleCollision(GameObject other)
        {
            Collider2D collision = other.GetComponent<Collider2D>();
            ApplyDamage(collision);
        }

        protected void ApplyDamage(Collider2D collision)
        {
            if (collision.attachedRigidbody == null)
            {
                AfterHit(TargetHitType.Unknown);
                return;
            }

            if (collision.attachedRigidbody.TryGetComponent(out IHittable hittable))
            {
                OnApplyDamage?.Invoke(hittable);
                Vector2 contact = collision.ClosestPoint(transform.position);
                Damage damage = createDamage();
                damage.AddHitBoxInfo(transform, contact);
                DamageInfo info = hittable.TakeDamage(damage);

                if (hittable is IDamageable damageable)
                {
                    TargetHitType targetHit = damageable.CurrentHealth <= 0 ? TargetHitType.KilledDamageable : TargetHitType.Damageable;
                    AfterHit(targetHit);
                    return;
                }

                AfterHit(TargetHitType.Hittable);
                return;
            }

            AfterHit(TargetHitType.Unknown);
        }

        protected virtual void AfterHit(TargetHitType targetHit) { }

        public static HitBox operator +(HitBox hitBox, DamageData damage)
        {
            hitBox.SetDamage(damage);
            return hitBox;
        }
    }
}
