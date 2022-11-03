using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public class FixedMovePS : PlayerAction
    {
        public BBParameter<float> moveSpeed;

        protected override void EnterState()
        {
            Camera.SetSensitivity(Settings.Physics.AimSensibility);
            Animator.SetAimWeight(1f);
        }

        protected override void OnUpdate()
        {
            Physics.FixedMove(moveSpeed.value);
        }

        protected override void ExitState() 
        {
            Camera.LockY(false);
            Camera.SetSensitivity(Settings.Physics.DefaultSensibility);
            Animator.SetAimWeight(0f);
        }
    }
}