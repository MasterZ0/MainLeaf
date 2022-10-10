using AdventureGame.Data;

namespace AdventureGame.Player.States
{
    public class AirRangeShootFallPS : PlayerAction
    {
        /*
        protected override bool CombatState => true;
        public bool shooting;

        protected override void EnterState()
        {

            shooting = false;
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

            base.OnExecute();

            Arsenal.SetWeaponPivot(this);
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

        protected override void TransitionToGround()
        {
            if (!shooting)
                SetState<StandRangeShootPS>();
            else
                SetState<IdlePS>();
        }

        protected override void StartFallingAnimation()
        {
            if (!shooting)
                Animator.AirChargeArrow();
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
                SetState<FallingPS>();
                return;
            }

            base.CheckConditions();
        }

        protected override void SetJumpState()
        {
            if (!shooting)
                SetState<AirRangeShootJumpPS>();
            else
                SetState<JumpPS>();
        }
        */
    }
}