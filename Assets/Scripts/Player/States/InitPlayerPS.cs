using AdventureGame.Data;
using Z3.NodeGraph.Core;

namespace AdventureGame.Player.States
{
    public abstract class InitPlayerPS<T> : PlayerAction where T : PlayerSettings
    {
        public Parameter<float> aimMoveSpeed;
        public Parameter<float> walkSpeed;
        public Parameter<float> sprintSpeed;

        public Parameter<float> jumpGravity;
        public Parameter<float> groundedGravity;
        public Parameter<float> fallingGravity;

        protected sealed override void EnterState()
        {
            T settings = Agent.PlayerSettings as T;
            Init(settings);

            aimMoveSpeed.Value = settings.Physics.AimMoveSpeed;
            walkSpeed.Value = settings.Physics.WalkSpeed;
            sprintSpeed.Value = settings.Physics.SprintSpeed;

            jumpGravity.Value = settings.Physics.JumpGravity;
            groundedGravity.Value = settings.Physics.GroundedGravity;
            fallingGravity.Value = settings.Physics.FallingGravity;

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