using AdventureGame.Data;
using AdventureGame.Shared.ExtensionMethods;
using NodeCanvas.Framework;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class AirSPS : PlayerAction
    {
        public BBParameter<bool> jumping;
        public BBParameter<string> overrideIdleState;

        public string landing = "Landing";
        private bool JumpPressed => Inputs.JumpPressed; // Check by event?
        private bool falling;

        #region Action
        protected override void EnterState()
        {
            falling = !jumping.value;

            if (falling)
            {
                Physics.SetGravityScale(Settings.Physics.FallingGravity);
                Animator.Falling();
            }
            else
            {
                //VFX.Trail(); Sprint?
                Animator.Jump();

                VFX.Jump();
                SFX.Jump();
                Physics.SetGravityScale(Settings.Physics.JumpGravity);
            }

            //Physics.NoFriction();
            //Animator.SetAirGroundBlend(0f);
        }

        protected override void OnUpdate()
        {
            // Update Air Velocity
            Vector2 velocity = Physics.Velocity;

            ApplyJump();

            if (!falling && velocity.y < 0) // Wait until start fall
            {
                falling = true;
                Physics.SetGravityScale(Settings.Physics.FallingGravity);
                Animator.Falling();
            }


            // Used to change idle animation
            //float yVelocity = Physics.Velocity.y;
            //if (yVelocity < Blackboard.BiggerFallingVelocity)
            //    Blackboard.BiggerFallingVelocity = yVelocity;
        }

        protected override void ExitState()
        {
            overrideIdleState.value = landing;
            VFX.Landing(); // TODO: Idle -> if (LastState<AirSPS>())
            //VFX.SetActiveTrail(false);
        }
        #endregion

        private void ApplyJump()
        {
            if (!jumping.value)
                return;

            Physics.Jump(PhysicsSettings.JumpVelocity);
            if (elapsedTime > PhysicsSettings.JumpRangeDuration.x) // Min Jump
            {
                if (!JumpPressed || elapsedTime >= PhysicsSettings.JumpRangeDuration.y) // Jump Stopped or Max Jump
                {
                    jumping.value = false;
                }
            }
        }
    }
}
