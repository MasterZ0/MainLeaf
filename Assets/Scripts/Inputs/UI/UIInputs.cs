using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventureGame.Inputs
{
    /// <summary>
    /// Send UI Inputs events
    /// </summary>
    public class UIInputs : BaseInput
    {
        public event Action OnPause = delegate { };
        public event Action OnSubmitPressed = delegate { };
        public event Action OnSubmitReleased = delegate { };
        public event Action OnCancel = delegate { };
        public event Action OnExtra = delegate { };
        public event Action<bool> OnSkipCutscene = delegate { };
        public event Action<Vector2> OnMove = delegate { };
        
        public Vector2 Direction
        {
            get
            {
                if (!controls.Player.enabled)

                    return Vector2.zero;
                Vector2 direction = controls.UI.Move.ReadValue<Vector2>();

                direction.x = Mathf.Round(direction.x);
                direction.y = Mathf.Round(direction.y);

                return direction;
            }
        }

        public UIInputs(bool enable = true) : base(enable)
        {
            controls.UI.Pause.started += OnPressPause;
            controls.UI.Submit.started += OnPressSubmit;
            controls.UI.Submit.canceled += OnReleaseSubmit;
            controls.UI.Cancel.started += OnPressCancel;
            controls.UI.ExtraButton.started += OnPressExtraButton;
            controls.UI.Move.started += OnPressMove;
            controls.UI.SkipCutscene.started += OnPressSkip;
            controls.UI.SkipCutscene.canceled += OnReleaseSkip;
        }

        private void OnPressSkip(InputAction.CallbackContext obj)
        {
            OnSkipCutscene(true);
        }

        private void OnReleaseSkip(InputAction.CallbackContext obj)
        {
            OnSkipCutscene(false);
        }
        
        private void OnPressMove(InputAction.CallbackContext obj)
        {
            Vector2 direction = obj.ReadValue<Vector2>();
            OnMove(direction);
        }

        private void OnPressPause(InputAction.CallbackContext obj)
        {
            OnPause();
        }
        
        private void OnPressSubmit(InputAction.CallbackContext obj)
        {
            OnSubmitPressed();
        }
        
        private void OnReleaseSubmit(InputAction.CallbackContext obj)
        {
            OnSubmitReleased();
        }
        
        private void OnPressCancel(InputAction.CallbackContext obj)
        {
            OnCancel();
        }
        
        private void OnPressExtraButton(InputAction.CallbackContext obj)
        {
            OnExtra();
        }
    }
}