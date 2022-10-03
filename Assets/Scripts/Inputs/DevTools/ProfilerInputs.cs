using System;
using UnityEngine.InputSystem;

namespace AdventureGame.Inputs
{
    public class ProfilerInputs : BaseInput
    {
        public event Action OnStatistics;

        public ProfilerInputs(bool enable = true) : base(enable)
        {
            controls.Debug.Statistics.started += OnStatisticsPressed;
        }

        private void OnStatisticsPressed(InputAction.CallbackContext _) => OnStatistics();
    }
}