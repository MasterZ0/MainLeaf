using UnityEngine.InputSystem;
using System;

namespace AdventureGame.Inputs
{
    /// Docs: 
    /// https://forum.unity.com/threads/check-if-any-key-is-pressed.763751/
    /// https://youtu.be/m5WsmlEOFiA
    /// https://forum.unity.com/threads/detect-most-recent-input-device-type.753206/   
    public class InputDetector : BaseInput
    {
        public event Action<DeviceController> onDeviceChange;
        public DeviceController CurrentInput { get; private set; }

        public InputDetector(bool enable = true) : base(enable)
        {
            if (InputSystem.devices.Count > 0)
            {
                CurrentInput = InputSystem.devices[0].ConvertToDeviceController();
            }
            //InputSystem.onActionChange += OnAnyButtonPress; // Idea

            InputAction action = new InputAction(type: InputActionType.PassThrough, binding: "*/<Button>");
            action.AddBinding("<Gamepad>/*");
            action.Enable();
            action.performed += AnyButton; 
        }
        private void AnyButton(InputAction.CallbackContext context)
        {
            InputDevice currentDevice = context.action.activeControl.device;
            DeviceController inputType = currentDevice.ConvertToDeviceController();

            if (CurrentInput != inputType)
            {
                CurrentInput = inputType;
                onDeviceChange(CurrentInput);
            }
        }
    }
}