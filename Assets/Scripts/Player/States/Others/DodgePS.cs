using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Player.States {
    
    public class DodgePS : PlayerAction {
        /*
        private float dodgeXVelocity;
        private float direction;
        private readonly Timer dodgeTimer = new Timer();

        #region Action
        protected override void EnterState()
        {
            Status.OnDodgeStart();

            direction = Physics.Direction;
            Vector2 velocity = new Vector2(PhysicsSettings.DodgeXVelocity, PhysicsSettings.DodgeYVelocity);
            dodgeXVelocity = velocity.x;
            Physics.SetVelocity(velocity);
            Physics.SetGravityScale(PhysicsSettings.AirGravity);
            Physics.NoFriction();

            Animator.Dodge();
            VFX.SetActiveTrail(true);
            VFX.FrontalDust();
            SFX.Dodge();
        }

        protected override void UpdateAction() 
        {
            dodgeXVelocity -= PhysicsSettings.DodgeDamp * Time.fixedDeltaTime;
            dodgeXVelocity = Mathf.Clamp(dodgeXVelocity, PhysicsSettings.MinimumDodgeXVelocity, float.MaxValue);
            Physics.SetVelocityX(dodgeXVelocity * -direction);
        }

        protected override void ExitState()
        {
            Status.OnDodgeEnd();
            SFX.Landing();
            VFX.SetActiveTrail(false);
            VFX.Landing();
        }
        #endregion

        #region Transitions
        protected override void EnterConditions()
        {
            dodgeTimer.Set(PhysicsSettings.MinDodgeDuration);
        }
        protected override void CheckConditions()
        {
            if (!dodgeTimer.IsCompleted)
            {
                dodgeTimer.FixedTick();
                return;
            }

            bool isGrounded = Physics.CheckAirToGround();
            if (isGrounded)
            {
                SwitchState<IdlePS>();
            }
        }

        protected override void DrawGizmos()
        {
            Physics.DrawGizmos();
        }
        #endregion*/
    }
}