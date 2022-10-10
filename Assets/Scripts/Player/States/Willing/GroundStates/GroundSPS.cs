using UnityEngine;

namespace AdventureGame.Player.States
{
    public class GroundSPS : PlayerAction 
    {

        protected override void EnterState()
        {
            Physics.SetGravityScale(Settings.Physics.GroundedGravity);
            Animator.SetMaxVelocityScale(Settings.Physics.SprintSpeed);
            //if (LastStateIs<AirSPS>())
            //{
            //    VFX.Landing();
            //}
            //Animator.SetAirGroundBlend(1f);
        }
    }
}
