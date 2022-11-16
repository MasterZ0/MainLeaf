using NodeCanvas.Framework;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class AirSPS : PlayerAction
    {
        public BBParameter<bool> jumping;

        public BBParameter<float> jumpGravity;
        public BBParameter<float> fallingGravity;

        private bool falling;

        private bool MinJumpApplied => elapsedTime > PhysicsSettings.JumpRangeDuration.x;
        private bool MaxJumpApplied => elapsedTime >= PhysicsSettings.JumpRangeDuration.y;
        private bool JumpPressed => Inputs.JumpPressed;

        #region Action
        protected override void EnterState()
        {
            falling = !jumping.value;

            if (falling)
            {
                Physics.SetGravityScale(fallingGravity.value);
                Animator.Falling();
            }
            else
            {
                //VFX.Trail(); Sprint?
                Animator.Jump();

                VFX.Jump();
                SFX.Jump();
                Physics.SetGravityScale(jumpGravity.value);
            }
        }

        protected override void OnUpdate()
        {
            if (jumping.value)
            {
                Physics.Jump(PhysicsSettings.JumpVelocity);
                if (MinJumpApplied && (!JumpPressed || MaxJumpApplied))
                {
                    jumping.value = false;
                }
                return;
            }

            if (!falling && MinJumpApplied && Physics.Velocity.y < 0) // Wait until start fall
            {
                falling = true;
                Physics.SetGravityScale(fallingGravity.value);
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
