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
        public Vector2 Directional 
        {
            get 
            {
                if (!controls.Player.enabled) 
                    return Vector2.zero;

                Vector2 direction = controls.Player.Move.ReadValue<Vector2>();
                direction.x = Mathf.Round(direction.x);
                direction.y = Mathf.Round(direction.y);

                return direction;
            }
        }
        
        public bool DirectionalPressed => Directional != Vector2.zero;
        public bool UpDirectionPressed => Directional.y > 0;
        public bool DownDirectionPressed => Directional.y < 0;
        public bool HorizontalDirectionPressed => Mathf.Abs(Directional.x) > 0;
        public bool VerticalDirectionPressed => Mathf.Abs(Directional.y) > 0;

        public bool PrimarySkillPressed { get; private set; }
        public bool SecondarySkillPressed { get; private set; }
        public bool JumpPressed { get; private set; }

        public event Action OnJumpUp = delegate { };
        public event Action OnJumpDown = delegate { };
        public event Action OnDashDown = delegate { };
        public event Action OnPrimarySkillDown = delegate { };
        public event Action OnPrimarySkillUp = delegate { };
        public event Action OnSecondarySkillDown = delegate { };
        public event Action OnSecondarySkillUp = delegate { };
        
        public PlayerInputs(bool enable = true) : base(InputManager.Controls, enable)
        {
            controls.Player.Jump.started += SendJumpDown;
            controls.Player.Jump.canceled += SendJumpUp;
            controls.Player.Dash.started += SendDashDown;
            controls.Player.PrimarySkill.started += SendPrimarySkillDown;
            controls.Player.PrimarySkill.canceled += SendPrimarySkillUp;
            controls.Player.SecondarySkill.started += SendSecondarySkillDown;
            controls.Player.SecondarySkill.canceled += SendSecondarySkillUp;
        }

        public override void Dispose()
        {
            controls.Player.Jump.started -= SendJumpDown;
            controls.Player.Jump.canceled -= SendJumpUp;
            controls.Player.Dash.started -= SendDashDown;
            controls.Player.PrimarySkill.started -= SendPrimarySkillDown;
            controls.Player.PrimarySkill.canceled -= SendPrimarySkillUp;
            controls.Player.SecondarySkill.started -= SendSecondarySkillDown;
            controls.Player.SecondarySkill.canceled -= SendSecondarySkillUp;
        }

        private void SendSecondarySkillDown(CallbackContext _) 
        {
            SecondarySkillPressed = true;
            OnSecondarySkillDown();
        }

        private void SendSecondarySkillUp(CallbackContext _) 
        {
            SecondarySkillPressed = false;
            OnSecondarySkillUp();
        }

        private void SendJumpDown(CallbackContext _) 
        {
            JumpPressed = true;
            OnJumpDown();
        }
        
        private void SendJumpUp(CallbackContext _) 
        {
            JumpPressed = false;
            OnJumpUp();
        }

        private void SendPrimarySkillDown(CallbackContext _)
        {
            PrimarySkillPressed = true;
            OnPrimarySkillDown();
        }

        private void SendPrimarySkillUp(CallbackContext _)
        {
            PrimarySkillPressed = false;
            OnPrimarySkillUp();
        }

        private void SendDashDown(CallbackContext _) => OnDashDown();
    }
}