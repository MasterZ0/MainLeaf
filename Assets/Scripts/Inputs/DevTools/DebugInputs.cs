using System;
using UnityEngine.InputSystem;

namespace AdventureGame.Inputs
{
    public class DebugInputs : BaseInput
    {
        public event Action OnGodModeDown;

        public DebugInputs(bool enable = true) : base(enable)
        {
            controls.Debug.GodMode.started += OnGodMode;
        }

        private void OnGodMode(InputAction.CallbackContext obj)
        {
            OnGodModeDown();
        }
    }
}