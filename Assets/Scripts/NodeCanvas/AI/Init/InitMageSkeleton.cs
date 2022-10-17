using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.AI
{
    public class InitMageSkeleton : InitEnemy<MageSkeletonSettings>
    {
        [Header("Patrol")]
        public BBParameter<float> idleTime;
        public BBParameter<float> patrolRadius;

        [Header("Movement")]
        public BBParameter<float> patrolMaxSpeed;
        public BBParameter<AIPathParameters> patrolParameters;
        public BBParameter<float> chaseMaxSpeed;
        public BBParameter<AIPathParameters> chaseParameters;
        public BBParameter<float> fleeMaxSpeed;
        public BBParameter<AIPathParameters> fleeParameters;

        [Header("Battle")]
        public BBParameter<float> chaseDistance;
        public BBParameter<float> distanceToAttack;
        public BBParameter<float> rotationSpeed;
        public BBParameter<float> angleDifferenceToAttack;
        public BBParameter<float> centerAttackAngle;
        public BBParameter<DamageData> fireballDamage;

        [Header("Delays")]
        public BBParameter<float> delayAfterAttack;
        public BBParameter<float> delayToReturnToPatrol;
        public BBParameter<float> delayToChase;

        public override MageSkeletonSettings EnemyData => GameSettings.Enemies.MageSkeletonSettings;

        protected override void SetParameters()
        {
            MageSkeletonSettings settings = EnemyData;

            // Patrol
            idleTime.value = settings.idleTime;
            patrolRadius.value = settings.patrolRadius;

            // Movement
            patrolMaxSpeed.value = settings.patrolParameters.maxSpeed;
            patrolParameters.value = settings.patrolParameters;
            chaseMaxSpeed.value = settings.chaseParameters.maxSpeed;
            chaseParameters.value = settings.chaseParameters;
            fleeMaxSpeed.value = settings.fleeParameters.maxSpeed;
            fleeParameters.value = settings.fleeParameters;

            // Battle
            chaseDistance.value = settings.chaseDistance;
            distanceToAttack.value = settings.distanceToAttack;
            rotationSpeed.value = settings.rotationSpeed;
            angleDifferenceToAttack.value = settings.angleDifferenceToAttack;
            centerAttackAngle.value = settings.centerAttackAngle;

            fireballDamage.value = settings.fireballDamage;

            // Delays
            delayAfterAttack.value = settings.delayAfterAttack;
            delayToReturnToPatrol.value = settings.delayToReturnToPatrol;
            delayToChase.value = settings.delayToChase;

        }
    }
}