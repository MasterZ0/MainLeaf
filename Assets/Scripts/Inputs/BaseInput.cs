using UnityEngine;

namespace AdventureGame.Inputs
{
    public abstract class BaseInput
    {
        protected readonly Controls controls;
        protected bool AfterActionFrame => Time.frameCount > activationFrame;

        private int activationFrame;

        public BaseInput(bool enable) : this(new Controls(), enable) { }
        public BaseInput(Controls controls, bool enable)
        {
            this.controls = controls;

            if (enable)
            {
                controls.Enable();
            }
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

        public virtual void Dispose()
        {
            controls.Dispose();
        }
    }
}