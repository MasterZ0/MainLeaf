using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Shared.NodeCanvas;
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
        public BBParameter<float> patrolMaxSpeed;
        public BBParameter<AIPathParameters> patrolParameters;

        [Header("Battle")]
        public BBParameter<float> chaseDistance;
        public BBParameter<float> battleMaxSpeed;
        public BBParameter<AIPathParameters> battleParameters;
        public BBParameter<float> distanceToAttack;
        public BBParameter<float> rotationSpeed;
        public BBParameter<float> angleDifferenceToAttack;
        public BBParameter<float> centerAttackAngle;

        [Header("Delays")]
        public BBParameter<float> delayAfterAttack;
        public BBParameter<float> delayToReturnToPatrol;
        public BBParameter<float> delayToChase;

        public override WarriorSkeletonSettings EnemyData => GameSettings.Enemies.WarriorSkeleton;

        protected override void SetParameters()
        {
            WarriorSkeletonSettings settings = EnemyData;

            // Components
            weaponHitBox.value.SetDamage(settings.swordDamage, agent);
            
            // Patrol
            idleTime.value = settings.idleTime;
            patrolRadius.value = settings.patrolRadius;
            patrolMaxSpeed.value = settings.patrolParameters.maxSpeed;
            patrolParameters.value = settings.patrolParameters;

            // Battle
            chaseDistance.value = settings.chaseDistance;
            battleMaxSpeed.value = settings.battleParameters.maxSpeed;
            battleParameters.value = settings.battleParameters;
            distanceToAttack.value = settings.distanceToAttack;
            rotationSpeed.value = settings.rotationSpeed;
            angleDifferenceToAttack.value = settings.angleDifferenceToAttack;
            centerAttackAngle.value = settings.centerAttackAngle;

            // Delays
            delayAfterAttack.value = settings.delayAfterAttack;
            delayToReturnToPatrol.value = settings.delayToReturnToPatrol;
            delayToChase.value = settings.delayToChase;
        }
    }
}