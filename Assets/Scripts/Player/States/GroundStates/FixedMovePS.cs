using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public class FixedMovePS : PlayerAction
    {
        public BBParameter<float> moveSpeed;

        protected override void EnterState()
        {
            agent.SetSensitivity(SensitivityType.Aim);
            Animator.SetAimWeight(1f);
        }

        protected override void OnUpdate()
        {
            Physics.FixedMove(moveSpeed.value);
        }

        protected override void ExitState() 
        {
            Camera.LockY(false);
            agent.SetSensitivity(SensitivityType.Default);
            Animator.SetAimWeight(0f);
        }
    }
}