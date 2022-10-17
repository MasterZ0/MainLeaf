using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsEnemies + "MageSkeleton", fileName = "MageSkeletonSettings")]
    public class MageSkeletonSettings : EnemyData
    {
        [Title("Mage Skeleton Settings")]
        [Title("Warrior Skeleton Settings")]
        public float idleTime = 2f;
        public float patrolRadius = 5f;

        [Header("Movement")]
        public AIPathParameters patrolParameters;
        public AIPathParameters fleeParameters;
        public AIPathParameters chaseParameters;

        [Header("Battle")]
        public float chaseDistance = 15f;
        public float rotationSpeed = 2f;
        [Range(0f, 180f)]
        public float angleDifferenceToAttack = 5f;
        public float centerAttackAngle = 30f;
        public float distanceToAttack;
        public DamageData fireballDamage;

        [Header("Delays")]
        public float delayAfterAttack = 1f;
        public float delayToReturnToPatrol = 2f;
        public float delayToChase = 1f;
    }
}