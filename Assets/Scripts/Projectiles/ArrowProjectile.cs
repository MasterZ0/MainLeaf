using AdventureGame.BattleSystem;
using AdventureGame.ObjectPooling;
using System.Collections;
using UnityEngine;

namespace AdventureGame.Projectiles
{
    public class ArrowProjectile : Projectile
    {
        private float delayToDisapear;

        private void Awake()
        {
            rigidbod.centerOfMass = transform.position;
        }

        private void OnEnable()
        {
            rigidbod.isKinematic = false;
        }

        public void Shoot(Damage damage, float velocity, float delayToDisapear)
        {
            this.delayToDisapear = delayToDisapear;
            Shoot(damage, velocity);
        }

        protected override void AfterHit(TargetHitType targetHit)
        {
            if (targetHit == TargetHitType.Unknown)
            {
                rigidbod.isKinematic = true;
                StartCoroutine(DelayToDisapear());
            }
            else if (targetHit == TargetHitType.Damageable)
            {
                Impact();
            }
        }

        IEnumerator DelayToDisapear()
        {
            yield return new WaitForSeconds(delayToDisapear);
            this.ReturnToPool();
        }

    }
}
