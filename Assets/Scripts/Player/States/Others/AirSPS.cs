using Z3.NodeGraph.Core;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class AirSPS : PlayerAction
    {
        public Parameter<bool> jumping;

        public Parameter<float> jumpGravity;
        public Parameter<float> fallingGravity;

        private bool falling;

        private bool MinJumpApplied => NodeRunningTime > PhysicsSettings.JumpRangeDuration.x;
        private bool MaxJumpApplied => NodeRunningTime >= PhysicsSettings.JumpRangeDuration.y;
        private bool JumpPressed => Inputs.JumpPressed;

        #region Action
        protected override void EnterState()
        {
            falling = !jumping.Value;

            if (falling)
            {
                Physics.SetGravityScale(fallingGravity.Value);
                Animator.Falling();
            }
            else
            {
                //VFX.Trail(); Sprint?
                Animator.Jump();

                VFX.Jump();
                SFX.Jump();
                Physics.SetGravityScale(jumpGravity.Value);
            }
        }

        protected override void UpdateAction()
        {
            if (jumping.Value)
            {
                Physics.Jump(PhysicsSettings.JumpVelocity);
                if (MinJumpApplied && (!JumpPressed || MaxJumpApplied))
                {
                    jumping.Value = false;
                }
                return;
            }

            if (!falling && MinJumpApplied && Physics.Velocity.y < 0) // Wait until start fall
            {
                falling = true;
                Physics.SetGravityScale(fallingGravity.Value);
                Animator.Falling();
            }
        }

        protected override void ExitState()
        {
            SFX.LandingSoft();
            VFX.Landing(); // TODO: Idle -> if (LastState<AirSPS>())
            //VFX.SetActiveTrail(false);
        }
        #endregion
    }
}
