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

        /// <summary> Change device or change the bindings </summary>
        public static event Action<DeviceController> OnUpdateDevice = delegate { };
        public static DeviceController CurrentDevice => inputDetector.CurrentInput;
        public static InputSpriteMapData Map { get; private set; }
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
            OnUpdateDevice(device);
        }

        public static void OnRebindControls() => OnUpdateDevice(inputDetector.CurrentInput); // Could be a different event

        public static Sprite GetPlayerIcon(InputActionReference inputActionReference, DeviceController device)
        {
            string schemeGroup = device.ConvertToControlSchemeGroup().ToString();

            InputAction correspondingAction = Controls.FindAction(inputActionReference.action.name);

            InputBinding binding = correspondingAction.bindings.FirstOrDefault(b => b.groups == schemeGroup);

            return GetPlayerIcon(binding.effectivePath, device);
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