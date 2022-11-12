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
        public event Action OnSubmit = delegate { };
        public event Action OnCancel = delegate { };
        public event Action OnExtra = delegate { };
        public event Action OnLeftTab = delegate { };
        public event Action OnRightTab = delegate { };
        public event Action<bool> OnSkipCutscene = delegate { };
        public event Action<Vector2> OnMove = delegate { };

        public UIInputs(bool enable = true) : base(enable)
        {
            controls.UI.Pause.started += OnPressPause;
            controls.UI.Submit.started += OnPressSubmit;
            controls.UI.Cancel.started += OnPressCancel;
            controls.UI.ExtraButton.started += OnPressExtraButton;
            controls.UI.Move.started += OnPressMove;
            controls.UI.LeftTab.started += OnPressLeftTab;
            controls.UI.RightTab.started += OnPressRightTab;
            controls.UI.SkipCutscene.started += OnPressSkip;
            controls.UI.SkipCutscene.canceled += OnReleaseSkip;
        }

        private void OnPressMove(InputAction.CallbackContext obj)
        {
            Vector2 direction = obj.ReadValue<Vector2>();
            OnMove(direction);
        }

        private void OnPressSubmit(InputAction.CallbackContext obj) => OnSubmit();
        private void OnPressCancel(InputAction.CallbackContext obj) => OnCancel();
        private void OnPressPause(InputAction.CallbackContext obj) => OnPause();
        private void OnPressSkip(InputAction.CallbackContext obj) => OnSkipCutscene(true);
        private void OnReleaseSkip(InputAction.CallbackContext obj) => OnSkipCutscene(false);
        private void OnPressExtraButton(InputAction.CallbackContext obj) => OnExtra();
        private void OnPressLeftTab(InputAction.CallbackContext obj) => OnLeftTab();
        private void OnPressRightTab(InputAction.CallbackContext obj) => OnRightTab();
    }
}