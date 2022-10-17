using AdventureGame.Shared.ExtensionMethods;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

namespace AdventureGame.Inputs
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class InputRebinderManager : MonoBehaviour
    {
        [Title("Input Rebinder Manager")]
        [SerializeField] private List<InputRebinder> inputRebinders;
        [Space]
        [Header("Inputs")]
        [SerializeField] private InputAction inputExcluding;
        [SerializeField] private InputAction gamepadCancelRebind;
        [SerializeField] private InputAction pcCancelRebind;

        /// <summary> Key: group, Value: name </summary>
        private readonly Dictionary<ControlSchemeGroup, string> cancelingBinds = new Dictionary<ControlSchemeGroup, string>();
        private InputRebinder currentInputRebinder;

        private Controls Controls => InputManager.Controls;
        protected virtual float TimeOut => 10f;

        protected virtual void Awake()
        {
            cancelingBinds[ControlSchemeGroup.PC] = pcCancelRebind.bindings[0].effectivePath;
            cancelingBinds[ControlSchemeGroup.Gamepad] = gamepadCancelRebind.bindings[0].effectivePath;

            foreach (InputRebinder rebinder in inputRebinders)
            {
                rebinder.Init(this);
            }
        }

        #region Save, Load and Reset
        protected abstract void SaveBindingOverrides(string json);
        protected void LoadBindingOverrides(string rebinds)
        {
            Controls.LoadBindingOverridesFromJson(rebinds);
            RefreshAllRebinderIcons();
            InputManager.UpdateBindings(rebinds);
        }

        private void Save()
        {
            string overridersAsJson = Controls.SaveBindingOverridesAsJson();
            SaveBindingOverrides(overridersAsJson);
            InputManager.UpdateBindings(overridersAsJson);
        }

        public void CleanInput(InputRebinder inputRebinder)
        {
            InputAction action = Controls.FindAction(inputRebinder.InputReference.action.name);
            action.ApplyBindingOverride(inputRebinder.BindingIndex, string.Empty);
            Save();

            RefreshRebinderIcon(inputRebinder, action);
        }

        protected void ResetAllInputs()
        {
            Controls.RemoveAllBindingOverrides();
            Save();
            RefreshAllRebinderIcons();
        }
        #endregion

        #region Rebind
        public void DoRebind(InputRebinder inputRebinder)
        {
            currentInputRebinder = inputRebinder;
            EventSystem.current.SetSelectedGameObject(null);

            int bindingIndex = inputRebinder.BindingIndex;
            InputAction action = Controls.FindAction(inputRebinder.InputReference.action.name);

            InputBinding binding = action.bindings[bindingIndex];
            if (!CheckDeviceIsAvailable(binding))
            {
                inputRebinder.Select();
                return;
            }

            bool controlsEnabled = Controls.asset.enabled;
            Controls.Disable();

            RebindingOperation rebindOperation = action.PerformInteractiveRebinding(bindingIndex);

            foreach (InputBinding excludingBinding in inputExcluding.bindings)
            {
                rebindOperation.WithControlsExcluding(excludingBinding.path);
            }

            ControlSchemeGroup schemeGroup = binding.groups.ConvertToEnum<ControlSchemeGroup>();

            rebindOperation // Auto: BindingGroup?, TargetBinding, Match 0.05 included by default
                .WithCancelingThrough(cancelingBinds[schemeGroup])
                .OnComplete(operation => OnRebindComplete(operation, controlsEnabled))
                .OnCancel(operation => OnRebindCancel(operation, controlsEnabled))
                .WithTimeout(TimeOut)
                .Start();
        }

        private void OnRebindCancel(RebindingOperation rebindOperation, bool controlsEnabled)
        {
            rebindOperation.Dispose();
            StartCoroutine(DelayToSelect(controlsEnabled));
        }

        private void OnRebindComplete(RebindingOperation rebindOperation, bool controlsEnabled)
        {
            Save();
            rebindOperation.Dispose();

            RefreshRebinderIcon(currentInputRebinder, rebindOperation.action);

            StartCoroutine(DelayToSelect(controlsEnabled));
        }

        private IEnumerator DelayToSelect(bool controlsEnabled)
        {
            yield return new WaitForEndOfFrame();
            currentInputRebinder.Select();
            currentInputRebinder = null;

            if (controlsEnabled)
            {
                Controls.Enable();
            }
        }
        #endregion
        private void RefreshAllRebinderIcons()
        {
            foreach (InputRebinder rebinder in inputRebinders)
            {
                InputAction correspondingAction = Controls.FindAction(rebinder.InputReference.action.name);
                RefreshRebinderIcon(rebinder, correspondingAction);
            }
        }

        private void RefreshRebinderIcon(InputRebinder currentInputRebinder, InputAction action)
        {
            int bindIndex = currentInputRebinder.BindingIndex;
            DeviceController deviceController = action.ConvertToDeviceController(bindIndex);
            Sprite icon = InputManager.GetPlayerIcon(action.bindings[bindIndex].effectivePath, deviceController);
            currentInputRebinder.RefreshIcon(icon);
        }

        private bool CheckDeviceIsAvailable(InputBinding binding)
        {
            //return InputSystem.devices.Contains(binding.GetDevice());
            if (binding.GetDevice() is Gamepad)
            {
                return Gamepad.current != null;
            }
            else
            {
                return Keyboard.current != null;
            }
        }

        // >>>>>>>>>>>> Ideas
        private void Utilities()
        {
            RebindingOperation rebindOperation = null;
            InputBinding inputBinding = rebindOperation.action.GetBindingForControl(rebindOperation.action.controls[0]).Value;
            int index = rebindOperation.action.GetBindingIndexForControl(rebindOperation.action.controls[0]);
            rebindOperation.action.ApplyBindingOverride(inputBinding); // Check other extensions

            InputAction action = null;
            string message = action.expectedControlType.ToString();
            string bindText = action.GetBindingDisplayString(1);

            //controls.asset.FindControlScheme("PC");
            //InputActionMap map = controls.asset.FindActionMap("Player");
            // InputAction actionR = controls.asset.FindAction(actionName); // >>>> Enum
        }
    }
}