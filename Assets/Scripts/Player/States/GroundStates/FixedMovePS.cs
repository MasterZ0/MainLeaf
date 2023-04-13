using Z3.NodeGraph.Core;

namespace AdventureGame.Player.States
{
    public class FixedMovePS : PlayerAction
    {
        public Parameter<float> moveSpeed;

        protected override void EnterState()
        {
            Agent.SetSensitivity(SensitivityType.Aim);
            Animator.SetAimWeight(1f);
        }

        protected override void UpdateAction()
        {
            Physics.FixedMove(moveSpeed.Value);
        }

        protected override void ExitState() 
        {
            Camera.LockY(false);
            Agent.SetSensitivity(SensitivityType.Default);
            Animator.SetAimWeight(0f);
        }
    }
}