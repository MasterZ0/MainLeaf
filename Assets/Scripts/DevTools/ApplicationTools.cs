using AdventureGame.Data;
using UnityEngine;
using AdventureGame.Inputs;
using System;

namespace AdventureGame.DevTools
{
    /// <summary>
    /// Control the Profiler and UI status
    /// </summary>
    public class ApplicationTools : MonoBehaviour
    {
        [CustomBox]
        [SerializeField] private ProfilerStatistics profiller;

        private ApplicationToolsInputs inputs;

        public static event Action OnToggleUI;
        public static bool ShowingUI { get; private set; } = true;

        public bool DebugProfiler => GameSettings.General.Profiler;

        private void Awake()
        {
            inputs = new ApplicationToolsInputs();
            inputs.OnStatistics += OnStatistics;
            inputs.OnToggleUI += OnUpdateUI;

            profiller.Init();
        }

        private void OnDestroy()
        {
            inputs.Dispose();        
        }
        
        private void OnStatistics()
        {
            if (!DebugProfiler)
                return;

            profiller.ToogleActivation();
        }

        private void OnUpdateUI()
        {
            if (!GameSettings.General.HideUI)
                return;

            ShowingUI = !ShowingUI;
            OnToggleUI();
        }

        private void Update()
        {
            profiller.Update();
        }
    }
}
