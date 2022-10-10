using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class DyingPS : PlayerAction
    {/*
        private readonly Timer time = new Timer();

        #region Action
        protected override void EnterState()
        {
            time.Set(PhysicsSettings.DyingMinTime);
            SFX.Death();
            Physics.NoFriction();
            Physics.SetGravityScale(PhysicsSettings.AirGravity);
            
            if (Blackboard.BackDamage)
            {
               Controller.transform.Rotate(0f, 180f, 0f); ;
            }

            Vector2 velocity = PhysicsSettings.DeathVelocity;
            velocity.x *= -Physics.Direction;

            Physics.SetVelocity(velocity);
            Animator.Dying();
        }
        #endregion
        
        #region Transition
        protected override void CheckConditions()
        {
            if (!time.IsCompleted)
            {
                time.FixedTick();
                return;
            }

            if (Physics.CheckGround())
            {
                SwitchState<PlayerState>();
            }
        }
        #endregion*/
    }
}