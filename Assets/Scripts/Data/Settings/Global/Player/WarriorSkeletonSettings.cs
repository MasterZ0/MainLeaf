using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsEnemies + "WarriorSkeleton", fileName = "WarriorSkeletonSettings")]
    public class WarriorSkeletonSettings : EnemyData
    {
        [Title("Warrior Skeleton")]
        public float idleTime = 2f;
        public float patrolRadius = 5f;

        [Header("Movementation")]
        public float rotationSpeed = 2f;
        public AIPathParameters battleParameters;
        public AIPathParameters patrolParameters;

        [Header("Battle")]
        public float chaseDistance = 15f;
        [Range(0f, 180f)]
        public float angleDifferenceToAttack = 5f;
        public float centerAttackAngle = 30f;
        public float distanceToAttack = 1.4f;
        public DamageData swordDamage;

        [Header("Delays")]
        public float delayAfterAttack = 1f;
        public float delayToReturnToPatrol = 2f;
        public float delayToChase = 1f;
    }
}