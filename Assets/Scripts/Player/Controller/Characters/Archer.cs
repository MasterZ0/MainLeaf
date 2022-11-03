using UnityEngine;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;
using AdventureGame.Projectiles;
using AdventureGame.ObjectPooling;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shake;
using AdventureGame.Gameplay;
using System.Collections;

namespace AdventureGame.Player
{
    public class Archer : MonoBehaviour
    {
        [Title("Archer")]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform arrowPoint;
        [Space]
        [SerializeField] private ShakeData shootShake;
        [SerializeField] private ArrowProjectile arrowPrefab;

        [Title("Temp")]
        [SerializeField] private DamageData tempDamage;
        [SerializeField] private Vector2 tempVelocity;
        [SerializeField] private float delay;

        public void OnShootArrow()
        {
            Shaker.RequestShake(shootShake);
            //playerController.SFX.Shoot?

            StartCoroutine(Wait());
        }

        private IEnumerator Wait() // IK Bug
        {
            yield return new WaitForFixedUpdate();

            ArrowProjectile arrow = ObjectPool.SpawnPooledObject(arrowPrefab, arrowPoint.position, arrowPoint.rotation);
            Damage damage = new Damage(tempDamage, playerController);

            float velocity = tempVelocity.RandomRange();
            arrow.Shoot(damage, velocity, delay);
        }

        public void OnFootStep()
        {
            playerController.SFX.FoodStep();
        }
    }
}
