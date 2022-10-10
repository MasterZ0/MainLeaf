using AdventureGame.Data;
using UnityEngine;
namespace AdventureGame.Player.States
{

    public class StandRangeShootPS : PlayerAction
    {/*
        protected override bool CombatState => true;
        public bool shooting;

        protected override void EnterState()
        {
            shooting = false;
            if (LastStateIs<AirRangeShootJumpPS>())
            {
                var lastState = (AirRangeShootJumpPS) Controller.LastState;
                shooting = lastState.shooting;
            }
            else if (LastStateIs<AirRangeShootFallPS>())
            {
                var lastState = (AirRangeShootFallPS) Controller.LastState;
                shooting = lastState.shooting;
            }
            else if (LastStateIs<CrouchRangeShootPS>())
            {
                var lastState = (CrouchRangeShootPS) Controller.LastState;
                shooting = lastState.shooting;
            }
            else if (LastStateIs<StandRangeShootPS>())
            {
                var lastState = (StandRangeShootPS) Controller.LastState;
                shooting = lastState.shooting;
            }
            if (!shooting)
            {
                if (!Arsenal.SpawnArrow())
                {
                    SwitchState<IdlePS>();
                    return;
                }
            }

            base.OnExecute();

            Physics.FullFriction();
            Arsenal.SetWeaponPivot(this);
            Animator.ChargeArrow();
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
                    Animator.ShootArrow();
                    Arsenal.ShootArrow();
                    FacilitatorBuffer.SendToBuffer(Blackboard.RangedShootCooldown, ActionWindowSettings.RangedShootCooldown);
                }
            }
        }

        protected override void OnJumpPressed()
        {
            if(!shooting)
                SetState<AirRangeShootJumpPS>();
            else
                SwitchState<JumpPS>();
        }

        protected override void ExitConditions()
        {
            Arsenal.DispawnArrow();
            base.ExitConditions();
        }

        protected override void CheckConditions()
        {
            if (shooting && Animator.State == AnimatorState.Standard)
            {
                SwitchState<IdlePS>();
                return;
            }

            if (Input.DownDirectionPressed)
            {
                if (!shooting)
                    SwitchState<CrouchRangeShootPS>();
                else
                    SwitchState<CrouchedIdlePS>();
                return;
            }
            base.CheckConditions();
        }*/
    }
}