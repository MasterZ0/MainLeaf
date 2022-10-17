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

        protected Damage Damage { get; private set; }

        public void SetDamage(DamageData damageData, IAttacker attacker) => Damage = new Damage(damageData, attacker);
        public void SetDamage(Damage damage) => Damage = damage;

        protected virtual void OnTriggerEnter(Collider collision)
        {
            ApplyDamage(collision);
        }

        protected virtual void OnParticleCollision(GameObject other)
        {
            Collider collision = other.GetComponent<Collider>();
            ApplyDamage(collision);
        }

        protected void ApplyDamage(Collider collision)
        {
            if (collision.attachedRigidbody == null)
            {
                AfterHit(TargetHitType.Unknown);
                return;
            }

            if (collision.attachedRigidbody.TryGetComponent(out IHittable hittable))
            {
                Vector3 contact = collision.ClosestPoint(transform.position);

                Damage.AddHitBoxInfo(this, contact); // TODO: Clone or create a new instance, contact is changing
                hittable.TakeDamage(Damage);

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
    }
}
