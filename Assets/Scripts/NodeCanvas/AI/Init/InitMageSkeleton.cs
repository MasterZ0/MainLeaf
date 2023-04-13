using AdventureGame.BattleSystem;
using AdventureGame.Data;
using Z3.NodeGraph.Core;
using UnityEngine;

namespace AdventureGame.NodeCanvas.AI
{
    public class InitMageSkeleton : InitEnemy<MageSkeletonSettings>
    {
        [Header("Patrol")]
        public Parameter<float> idleTime;
        public Parameter<float> patrolRadius;
        public Parameter<float> delayToReturnToPatrol;

        [Header("Movement")]
        public Parameter<float> ikWeightTransition;
        public Parameter<float> rotationSpeed;
        public Parameter<float> patrolMaxSpeed;
        public Parameter<AIPathParameters> patrolParameters;
        public Parameter<float> chaseMaxSpeed;
        public Parameter<AIPathParameters> chaseParameters;
        public Parameter<float> fleeMaxSpeed;
        public Parameter<AIPathParameters> fleeParameters;

        [Header("Battle")]
        public Parameter<Vector2> offensiveDistanceRange;
        public Parameter<float> escapeDistance;
        public Parameter<float> chaseDistance;
        public Parameter<float> angleDifferenceToAttack;
        public Parameter<float> fireballVelocity;
        public Parameter<DamageData> fireballDamage;
        public Parameter<float> delayAfterAttack;

        protected override void SetParameters()
        {
            MageSkeletonSettings settings = EnemyData;

            // Patrol
            idleTime.Value = settings.idleTime;
            patrolRadius.Value = settings.patrolRadius;

            // Movement
            ikWeightTransition.Value = settings.ikWeightTransition;
            rotationSpeed.Value = settings.rotationSpeed;
            patrolMaxSpeed.Value = settings.patrolParameters.maxSpeed;
            patrolParameters.Value = settings.patrolParameters;
            chaseMaxSpeed.Value = settings.chaseParameters.maxSpeed;
            chaseParameters.Value = settings.chaseParameters;
            fleeMaxSpeed.Value = settings.fleeParameters.maxSpeed;
            fleeParameters.Value = settings.fleeParameters;

            // Battle
            chaseDistance.Value = settings.chaseDistance;
            offensiveDistanceRange.Value = settings.offensiveDistanceRange;
            escapeDistance.Value = settings.escapeDistance;
            angleDifferenceToAttack.Value = settings.angleDifferenceToAttack;
            fireballVelocity.Value = settings.fireballVelocity;
            fireballDamage.Value = settings.fireballDamage;

            // Delays
            delayAfterAttack.Value = settings.delayAfterAttack;
            delayToReturnToPatrol.Value = settings.delayToReturnToPatrol;
        }
    }
}