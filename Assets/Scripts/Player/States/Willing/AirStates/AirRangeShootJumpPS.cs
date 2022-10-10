using AdventureGame.Data;
using UnityEngine;

namespace AdventureGame.Player.States
{
    public class AirRangeShootJumpPS : PlayerAction
    {
        /*
        protected override bool CombatState => true;
        public bool shooting;
        private bool jumping;

        #region Action
        protected override void EnterState()
        {
            shooting = false;

            if (!LastStateIs<JumpPS>() || jumping)
            {
                base.OnExecute();
                jumping = false;
            }

            if (LastStateIs<AirRangeShootJumpPS>())
            {
                var lastState = (AirRangeShootJumpPS)Controller.LastState;
                shooting = lastState.shooting;
            }
            else if (LastStateIs<AirRangeShootFallPS>())
            {
                var lastState = (AirRangeShootFallPS)Controller.LastState;
                shooting = lastState.shooting;
            }
            else if (LastStateIs<CrouchRangeShootPS>())
            {
                var lastState = (CrouchRangeShootPS)Controller.LastState;
                shooting = lastState.shooting;
            }
            else if (LastStateIs<StandRangeShootPS>())
            {
                var lastState = (StandRangeShootPS)Controller.LastState;
                shooting = lastState.shooting;
            }

            if (!shooting)
            {
                if (!Arsenal.SpawnArrow())
                {
                    SetState<IdlePS>();
                    return;
                }
            }

            PlayJumpAnimation();

            Arsenal.SetWeaponPivot(this);
        }

        protected override void PlayJumpAnimation()
        {
            Animator.AirChargeArrow();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (!shooting)
            {
                Physics.UpdateFlip(Input.Direction.x);

                if (!Input.PrimarySkillPressed)
                {
                    shooting = true;
                    Animator.AirShootArrow();
                    Arsenal.ShootArrow();
                    FacilitatorBuffer.SendToBuffer(Blackboard.RangedShootCooldown, ActionWindowSettings.RangedShootCooldown);
                }
            }
        }
        #endregion

        #region Transitions
        protected override void ExitConditions()
        {
            Arsenal.DispawnArrow();
            base.ExitConditions();
        }

        protected override void TransitionToFalling()
        {
            SetState<AirRangeShootFallPS>();
        }

        protected override void SetJumpState()
        {
            if (!shooting)
            {
                jumping = true;
                SetState<AirRangeShootJumpPS>();
            }
            else
                SetState<JumpPS>();
        } 
        #endregion
        */
    }
}