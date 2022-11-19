using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Linq;
using AdventureGame.Inputs.Data;

namespace AdventureGame.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputSpriteMapData map;

        public static event Action<DeviceController> OnChangeDevice = delegate { };
        public static event Action OnChangeBindings;
        public static DeviceController CurrentDevice => inputDetector.CurrentInput;
        public static string OverrideBindings { get; private set; } = string.Empty;
        public static Controls Controls
        {
            get
            {
                if (controls == null)
                {
                    controls = new Controls();
                }
                return controls;
            }
        }
        private static InputSpriteMapData Map { get; set; }

        private static Controls controls;
        private static InputDetector inputDetector;

        private void Awake()
        {
            Map = map;
            inputDetector = new InputDetector();
            inputDetector.onDeviceChange += OnDeviceChange;

            OnDeviceChange(inputDetector.CurrentInput);
        }

        private void OnDestroy()
        {
            inputDetector.Dispose();
        }

        private void OnDeviceChange(DeviceController device)
        {
            OnChangeDevice(device);
        }

        public static void SetVibration(bool enabled) { } // TODO

        public static Sprite GetPlayerIcon(InputActionReference inputActionReference, DeviceController device)
        {
            string schemeGroup = device.ConvertToControlSchemeGroup().ToString();

            InputAction correspondingAction = Controls.FindAction(inputActionReference.action.name);

            InputBinding binding = correspondingAction.bindings.FirstOrDefault(b => b.groups == schemeGroup);

            return GetPlayerIcon(binding.effectivePath, device);
        }

        public static void UpdateBindings(string bindings)
        {
            OverrideBindings = bindings;
            OnChangeBindings();
        }

        public static Sprite GetPlayerIcon(string effectivePath, DeviceController inputType)
        {
            InputSpriteDeviceMap pack = inputType switch
            {
                DeviceController.PC => Map.pc,
                DeviceController.Playstation => Map.playstation,
                DeviceController.Xbox => Map.xbox,
                DeviceController.Nintendo => Map.nintendo,
                _ => throw new NotImplementedException(),
            };

            if (string.IsNullOrEmpty(effectivePath))
                return pack.undefined;

            string readablePath = InputControlPath.ToHumanReadableString(effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);

            Sprite sprite = pack.inputs.FirstOrDefault(i => i.path == readablePath)?.image;
            return sprite ? sprite : pack.unregistered;
        }
    }
}