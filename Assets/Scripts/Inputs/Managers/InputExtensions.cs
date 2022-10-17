using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.Switch;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Composites;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;
using System;
using AdventureGame.Shared.ExtensionMethods;

namespace AdventureGame.Inputs
{
    public static class InputExtensions
    {
        public static DeviceController ConvertToDeviceController(this InputAction action, int bindingIndex = -1)
        {
            return ConvertToDeviceController(action.bindings[bindingIndex]);
        }

        public static DeviceController ConvertToDeviceController(this InputBinding binding)
        {
            return binding.GetDevice().ConvertToDeviceController();
        }

        public static ControlSchemeGroup ConvertToControlSchemeGroup(this DeviceController deviceController)
        {
            return deviceController == DeviceController.PC ? ControlSchemeGroup.PC : ControlSchemeGroup.Gamepad;
        }

        public static InputDevice GetDevice(this InputBinding binding) // TODO: Improve
        {
            // Suggestion: Is possible to see in RebindingOperation that m_IncludePaths has information about the required devices
            // Suggestion: Check required devices by scheme Controls.PCScheme.deviceRequirements[0].controlPath

            InputDevice device = null;
            if (!string.IsNullOrEmpty(binding.effectivePath))
            {
                device = InputSystem.FindControl(binding.effectivePath)?.device;
            }

            if (device == null)
            {
                ControlSchemeGroup schemeGroup = binding.groups.ConvertToEnum<ControlSchemeGroup>();
                device = schemeGroup == ControlSchemeGroup.Gamepad ? new Gamepad() : new Keyboard();
            }
            
            return device;
        }

        public static DeviceController ConvertToDeviceController(this InputDevice device)
        {
            if (device is DualShockGamepad)
            {
                return DeviceController.Playstation;
            }
            else if (device is Mouse || device is Keyboard)
            {
                return DeviceController.PC;
            }
            else if (device is SwitchProControllerHID)
            {
                return DeviceController.Nintendo;
            }

            return DeviceController.Xbox; // XInputController
        }
    }
}