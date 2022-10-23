using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Shared.ExtensionMethods;
using NodeCanvas.Framework;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.AI
{
    public class InitMageSkeleton : InitEnemy<MageSkeletonSettings>
    {
        [Header("Patrol")]
        public BBParameter<float> idleTime;
        public BBParameter<float> patrolRadius;
        public BBParameter<float> delayToReturnToPatrol;

        [Header("Movement")]
        public BBParameter<float> ikWeightTransition;
        public BBParameter<float> rotationSpeed;
        public BBParameter<float> patrolMaxSpeed;
        public BBParameter<AIPathParameters> patrolParameters;
        public BBParameter<float> chaseMaxSpeed;
        public BBParameter<AIPathParameters> chaseParameters;
        public BBParameter<float> fleeMaxSpeed;
        public BBParameter<AIPathParameters> fleeParameters;

        [Header("Battle")]
        public BBParameter<Vector2> offensiveDistanceRange;
        public BBParameter<float> escapeDistance;
        public BBParameter<float> chaseDistance;
        public BBParameter<float> angleDifferenceToAttack;
        public BBParameter<float> fireballVelocity;
        public BBParameter<DamageData> fireballDamage;
        public BBParameter<float> delayAfterAttack;

        public override MageSkeletonSettings EnemyData => GameSettings.Enemies.MageSkeletonSettings;

        protected override void SetParameters()
        {
            MageSkeletonSettings settings = EnemyData;

            // Patrol
            idleTime.value = settings.idleTime;
            patrolRadius.value = settings.patrolRadius;

            // Movement
            ikWeightTransition.value = settings.ikWeightTransition;
            rotationSpeed.value = settings.rotationSpeed;
            patrolMaxSpeed.value = settings.patrolParameters.maxSpeed;
            patrolParameters.value = settings.patrolParameters;
            chaseMaxSpeed.value = settings.chaseParameters.maxSpeed;
            chaseParameters.value = settings.chaseParameters;
            fleeMaxSpeed.value = settings.fleeParameters.maxSpeed;
            fleeParameters.value = settings.fleeParameters;

            // Battle
            chaseDistance.value = settings.chaseDistance;
            offensiveDistanceRange.value = settings.offensiveDistanceRange;
            escapeDistance.value = settings.escapeDistance;
            angleDifferenceToAttack.value = settings.angleDifferenceToAttack;
            fireballVelocity.value = settings.fireballVelocity;
            fireballDamage.value = settings.fireballDamage;

            // Delays
            delayAfterAttack.value = settings.delayAfterAttack;
            delayToReturnToPatrol.value = settings.delayToReturnToPatrol;
        }
    }
}