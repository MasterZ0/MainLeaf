using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class DashPS : PlayerAction
    {

        private readonly Timer dashTimer = new Timer();

        #region Action
        protected override void EnterState()
        {
            //Status.OnDashStart();
            //Physics.SetGravityScale(0f);
            //Animator.DashStart();

            //VFX.SetActiveTrail(true);
            //VFX.GroundDash();
            SFX.Dash();
        }

        protected override void OnUpdate()
        {
            //Physics.SetGroundDashVelocity(Physics.Direction, PhysicsSettings.GroundDashVelocity);
        }

        protected override void ExitState()
        {
            //Status.OnDashEnd();
            //Physics.SetVelocity(Vector2.zero);
            //VFX.SetActiveTrail(false);
            //Physics.DisableLastBox();
            
            //if (!Physics.CheckGround())
            //{
            //    Animator.SetAirGroundBlend(0f);
            //}
        }
        #endregion
    }
}
