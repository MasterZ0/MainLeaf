using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventureGame.Inputs
{
    public class ScrollInput : BaseInput
    {
        public event Action<float> onMove = delegate { };

        public ScrollInput(bool enable = true) : base(enable)
        {
            controls.UI.Scroll.started += OnMove;
            controls.UI.Scroll.canceled += OnMove;
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            float direction = obj.ReadValue<Vector2>().y;
            onMove(direction);
        }
    }
}