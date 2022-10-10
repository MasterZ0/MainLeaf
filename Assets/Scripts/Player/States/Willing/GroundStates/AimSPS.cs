namespace AdventureGame.Player.States
{
    public class AimSPS : PlayerAction
    {

        protected override void EnterState()
        {
            Physics.SetGravityScale(Settings.Physics.GroundedGravity);
            Animator.SetMaxVelocityScale(Settings.Physics.AimMoveSpeed);
            //if (LastStateIs<AirSPS>())
            //{
            //    VFX.Landing();
            //}
            //Animator.SetAirGroundBlend(1f);
        }
    }
}
