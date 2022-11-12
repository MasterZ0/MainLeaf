using UnityEngine;
using UnityEngine.InputSystem;

namespace AdventureGame.Inputs
{
    public abstract class BaseInput
    {
        protected readonly Controls controls;
        protected bool AfterActionFrame => Time.frameCount > activationFrame;

        private int activationFrame;

        public BaseInput(bool enable)
        {
            controls = new Controls();
            OnUpdateBindings();
            InputManager.OnChangeBindings += OnUpdateBindings;

            if (enable)
            {
                controls.Enable();
            }
        }

        private void OnUpdateBindings()
        {
            controls.LoadBindingOverridesFromJson(InputManager.OverrideBindings);
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                controls.Enable();
            }
            else
            {
                controls.Disable();
            }

            activationFrame = Time.frameCount + 1; // +1 Avoid bug
        }

        public void Dispose()
        {
            InputManager.OnChangeBindings -= OnUpdateBindings;
            controls.Dispose();
        }
    }
}