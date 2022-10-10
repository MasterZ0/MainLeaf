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

        public void SetDamage(Damage damage)
        {
            Damage = damage;
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
                Vector2 contact = collision.ClosestPoint(transform.position);

                Damage.AddHitBoxInfo(this, contact);
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

        public static HitBox operator +(HitBox hitBox, Damage damage)
        {
            hitBox.SetDamage(damage);
            return hitBox;
        }
    }
}
