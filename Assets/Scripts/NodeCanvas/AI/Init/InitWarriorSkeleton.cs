using AdventureGame.BattleSystem;
using AdventureGame.Data;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.AI
{
    public class InitWarriorSkeleton : InitEnemy<WarriorSkeletonSettings>
    {
        [Header("Components")]
        public BBParameter<HitBox> weaponHitBox;

        [Header("Patrol")]
        public BBParameter<float> idleTime;
        public BBParameter<float> patrolRadius;
        public BBParameter<float> delayToReturnToPatrol;

        [Header("Movement")]
        public BBParameter<float> ikWeightTransition;
        public BBParameter<float> rotationSpeed;
        public BBParameter<float> patrolMaxSpeed;
        public BBParameter<AIPathParameters> patrolParameters;
        public BBParameter<float> battleMaxSpeed;
        public BBParameter<AIPathParameters> battleParameters;

        [Header("Battle")]
        public BBParameter<float> chaseDistance;
        public BBParameter<float> distanceToAttack;
        public BBParameter<float> angleDifferenceToAttack;
        public BBParameter<float> centerAttackAngle;
        public BBParameter<float> delayAfterAttack;

        public override WarriorSkeletonSettings EnemyData => GameSettings.Enemies.WarriorSkeleton;

        protected override void SetParameters()
        {
            WarriorSkeletonSettings settings = EnemyData;

            // Components
            weaponHitBox.value.SetDamage(settings.swordDamage, agent);
            
            // Patrol
            idleTime.value = settings.idleTime;
            patrolRadius.value = settings.patrolRadius;
            delayToReturnToPatrol.value = settings.delayToReturnToPatrol;

            // Movement
            ikWeightTransition.value = settings.ikWeightTransition;
            patrolMaxSpeed.value = settings.patrolParameters.maxSpeed;
            patrolParameters.value = settings.patrolParameters;
            battleMaxSpeed.value = settings.battleParameters.maxSpeed;
            battleParameters.value = settings.battleParameters;

            // Battle
            chaseDistance.value = settings.chaseDistance;
            distanceToAttack.value = settings.distanceToAttack;
            rotationSpeed.value = settings.rotationSpeed;
            angleDifferenceToAttack.value = settings.angleDifferenceToAttack;
            centerAttackAngle.value = settings.centerAttackAngle;
            delayAfterAttack.value = settings.delayAfterAttack;
        }
    }
}