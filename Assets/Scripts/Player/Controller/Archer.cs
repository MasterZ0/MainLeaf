using UnityEngine;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;
using AdventureGame.Projectiles;
using AdventureGame.ObjectPooling;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shake;

namespace AdventureGame.Player
{
    public class Archer : MonoBehaviour
    {
        [Title("Archer")]
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Transform arrowPoint;
        [SerializeField] private Transform defaultArrowPoint;
        [SerializeField] private Transform aimCamera;
        [Space]
        [SerializeField] private ShakeData shootShake;
        [SerializeField] private ArrowProjectile arrowPrefab;
        [SerializeField] private LayerMask hittable;

        [Title("Temp")]
        [SerializeField] private DamageData tempDamage;
        [SerializeField] private Vector2 tempVelocity;
        [SerializeField] private float delay;
        [SerializeField] private float arrowMaxDistance;


        public void OnShootArrow()
        {
            Shaker.RequestShake(shootShake);

            bool successful = Physics.Raycast(aimCamera.position, aimCamera.forward, out RaycastHit hit, arrowMaxDistance, hittable);
            Quaternion rotation;
            if (successful)
            {
                arrowPoint.LookAt(hit.point);
                rotation = arrowPoint.rotation;
            }
            else
            {
                rotation = defaultArrowPoint.rotation;
            }

            ArrowProjectile arrow = ObjectPool.SpawnPooledObject(arrowPrefab, arrowPoint.position, rotation);

            Damage damage = new Damage(tempDamage, playerController);

            float velocity = tempVelocity.RandomRange();
            arrow.Shoot(damage, velocity, delay);
            //playerController.SFX.Shoot?

            
        }

        public void OnFootStep()
        {
            playerController.SFX.FoodStep();
        }
    }
}
