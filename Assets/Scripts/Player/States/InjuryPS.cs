using AdventureGame.Shared;
using UnityEngine;

namespace AdventureGame.Player.States {
    
    public class InjuryPS : PlayerAction // Get variable and after start set to null
    {
        private readonly Timer stunTimer = new Timer();

        protected override void EnterState()
        {
            //if (Blackboard.BackDamage)
            //{
            //    Animator.InjureBack();
            //}
            //else
            //{
            //    Animator.Injure();
            //}
            
            SFX.Injury();
            //Physics.SetVelocity(Vector3.zero);
            Physics.FullFriction();
        }

        protected override void ExitState()
        {
            //Controller.InjuryStunEnd();
        }

        /*
        #region Transitions

        protected override void EnterConditions()
        {
            stunTimer.Set(PhysicsSettings.SlightInjuryTime);
            stunTimer.OnCompleted += EndState;
        }

        protected override void ExitConditions()
        {
            stunTimer.OnCompleted -= EndState;
        }

        protected override void CheckConditions()
        {
            stunTimer.FixedTick();
        }

        private void EndState()
        {
            if (Physics.CheckGround())
            {
                SwitchState<IdlePS>();
            }
            else
            {
                //SwitchState<FallingPS>();
            }
        }
        #endregion
        */
    }
}