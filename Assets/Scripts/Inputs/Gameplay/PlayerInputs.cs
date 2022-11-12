using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace AdventureGame.Inputs
{
    /// <summary>
    /// Send Gameplay input events
    /// </summary>
    public class PlayerInputs : BaseInput
    {
        public event Action<Vector2> OnMoveCamera = delegate { };
        public event Action OnJumpReleased = delegate { };
        public event Action OnJumpPressed = delegate { };
        public event Action OnDashPressed = delegate { };
        public event Action OnDashReleased = delegate { };
        public event Action OnPrimarySkillPressed = delegate { };
        public event Action OnPrimarySkillReleased = delegate { };
        public event Action OnSecondarySkillPressed = delegate { };
        public event Action OnSecondarySkillReleased = delegate { };

        public bool MovePressed => Move != Vector2.zero;
        public bool JumpPressed { get; private set; }
        public bool DashPressed { get; private set; }
        public bool PrimarySkillPressed { get; private set; }
        public bool SecondarySkillPressed { get; private set; }

        public Vector2 Move => controls.Player.Move.ReadValue<Vector2>();
        public Vector2 Look => controls.Player.Look.ReadValue<Vector2>();

        public PlayerInputs(bool enable = true) : base(enable)
        {
            controls.Player.Jump.started += OnJumpDown;
            controls.Player.Jump.canceled += OnJumpUp;
            controls.Player.Dash.started += OnDashDown;
            controls.Player.Dash.canceled += OnDashUp;
            controls.Player.PrimarySkill.started += SendPrimarySkillDown;
            controls.Player.PrimarySkill.canceled += SendPrimarySkillUp;
            controls.Player.SecondarySkill.started += SendSecondarySkillDown;
            controls.Player.SecondarySkill.canceled += SendSecondarySkillUp;
            controls.Player.Look.started += OnLook;
        }

        private void OnLook(CallbackContext ctx) => OnMoveCamera(ctx.ReadValue<Vector2>());

        private void OnJumpDown(CallbackContext _)
        {
            JumpPressed = true;
            OnJumpPressed();
        }

        private void OnJumpUp(CallbackContext _)
        {
            JumpPressed = false;
            OnJumpReleased();
        }

        private void OnDashDown(CallbackContext _)
        {
            DashPressed = true;
            OnDashPressed();
        }

        private void OnDashUp(CallbackContext _)
        {
            DashPressed = false;
            OnDashReleased();
        }

        private void SendPrimarySkillDown(CallbackContext _)
        {
            PrimarySkillPressed = true;
            OnPrimarySkillPressed();
        }

        private void SendPrimarySkillUp(CallbackContext _)
        {
            PrimarySkillPressed = false;
            OnPrimarySkillReleased();
        }

        private void SendSecondarySkillDown(CallbackContext _)
        {
            SecondarySkillPressed = true;
            OnSecondarySkillPressed();
        }

        private void SendSecondarySkillUp(CallbackContext _)
        {
            SecondarySkillPressed = false;
            OnSecondarySkillReleased();
        }
    }
}