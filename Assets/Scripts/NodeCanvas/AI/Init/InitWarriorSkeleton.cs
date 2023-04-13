using AdventureGame.BattleSystem;
using AdventureGame.Data;
using UnityEngine;
using Z3.NodeGraph.Core;

namespace AdventureGame.NodeCanvas.AI
{
    public class InitWarriorSkeleton : InitEnemy<WarriorSkeletonSettings>
    {
        [Header("Components")]
        public Parameter<HitBox> weaponHitBox;

        [Header("Patrol")]
        public Parameter<float> idleTime;
        public Parameter<float> patrolRadius;
        public Parameter<float> delayToReturnToPatrol;

        [Header("Movement")]
        public Parameter<float> ikWeightTransition;
        public Parameter<float> rotationSpeed;
        public Parameter<float> patrolMaxSpeed;
        public Parameter<AIPathParameters> patrolParameters;
        public Parameter<float> battleMaxSpeed;
        public Parameter<AIPathParameters> battleParameters;

        [Header("Battle")]
        public Parameter<float> chaseDistance;
        public Parameter<float> distanceToAttack;
        public Parameter<float> angleDifferenceToAttack;
        public Parameter<float> centerAttackAngle;
        public Parameter<float> delayAfterAttack;

        protected override void SetParameters()
        {
            WarriorSkeletonSettings settings = EnemyData;

            // Components
            weaponHitBox.Value.SetDamage(settings.swordDamage, Agent);
            
            // Patrol
            idleTime.Value = settings.idleTime;
            patrolRadius.Value = settings.patrolRadius;
            delayToReturnToPatrol.Value = settings.delayToReturnToPatrol;

            // Movement
            ikWeightTransition.Value = settings.ikWeightTransition;
            patrolMaxSpeed.Value = settings.patrolParameters.maxSpeed;
            patrolParameters.Value = settings.patrolParameters;
            battleMaxSpeed.Value = settings.battleParameters.maxSpeed;
            battleParameters.Value = settings.battleParameters;

            // Battle
            chaseDistance.Value = settings.chaseDistance;
            distanceToAttack.Value = settings.distanceToAttack;
            rotationSpeed.Value = settings.rotationSpeed;
            angleDifferenceToAttack.Value = settings.angleDifferenceToAttack;
            centerAttackAngle.Value = settings.centerAttackAngle;
            delayAfterAttack.Value = settings.delayAfterAttack;
        }
    }
}