using AdventureGame.Data;
using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public abstract class InitPlayerPS<T> : PlayerAction where T : PlayerSettings
    {
        public BBParameter<float> aimMoveSpeed;
        public BBParameter<float> walkSpeed;
        public BBParameter<float> sprintSpeed;

        public BBParameter<float> jumpGravity;
        public BBParameter<float> groundedGravity;
        public BBParameter<float> fallingGravity;

        protected sealed override void EnterState()
        {
            T settings = agent.PlayerSettings as T;
            Init(settings);

            aimMoveSpeed.value = settings.Physics.AimMoveSpeed;
            walkSpeed.value = settings.Physics.WalkSpeed;
            sprintSpeed.value = settings.Physics.SprintSpeed;

            jumpGravity.value = settings.Physics.JumpGravity;
            groundedGravity.value = settings.Physics.GroundedGravity;
            fallingGravity.value = settings.Physics.FallingGravity;

            EndAction(true);
        }

        protected abstract void Init(T settings);
    }

    public class InitArcherPS : InitPlayerPS<PlayerSettings>
    {
        protected override void Init(PlayerSettings settings) { }
    }
    public class InitWarriorPS : InitPlayerPS<PlayerSettings>
    {
        protected override void Init(PlayerSettings settings) { }
    }
    public class InitMagePS : InitPlayerPS<PlayerSettings>
    {
        protected override void Init(PlayerSettings settings) { }
    }
}