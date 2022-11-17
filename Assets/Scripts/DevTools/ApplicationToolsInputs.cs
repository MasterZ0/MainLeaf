using AdventureGame.Inputs;
using System;
using UnityEngine.InputSystem;

namespace AdventureGame.DevTools
{
    public class ApplicationToolsInputs : BaseInput
    {
        public event Action OnStatistics;
        public event Action OnToggleUI;

        public ApplicationToolsInputs(bool enable = true) : base(enable)
        {
            controls.Debug.Statistics.started += OnStatisticsPressed;
            controls.Debug.ToggleUI.started += OnToggleUIPressed;
        }

        private void OnStatisticsPressed(InputAction.CallbackContext _) => OnStatistics();
        private void OnToggleUIPressed(InputAction.CallbackContext _) => OnToggleUI();
    }
}