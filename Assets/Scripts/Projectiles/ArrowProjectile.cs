using AdventureGame.BattleSystem;
using AdventureGame.ObjectPooling;
using System.Collections;
using UnityEngine;

namespace AdventureGame.Projectiles
{
    public class ArrowProjectile : Projectile
    {
        [SerializeField] private Collider col;

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

        protected override void AfterHit(TargetHitType targetHit, Vector3 contact)
        {
            if (targetHit == TargetHitType.Unknown)
            {
                transform.position = transform.forward * -col.bounds.size.z + contact;
                rigidbod.isKinematic = true;
                StartCoroutine(DelayToDisapear());
            }
            else if (targetHit == TargetHitType.Alive)
            {
                Impact();
            }
        }

        private IEnumerator DelayToDisapear()
        {
            yield return new WaitForSeconds(delayToDisapear);
            this.ReturnToPool();
        }
    }
}
