using Z3.UIBuilder.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using AdventureGame.Shared;

namespace AdventureGame.Data
{
    public enum EnvironmentState {
        Develop,
        Staging,
        Release
    }

    /// <summary>
    /// Storage all data and variables
    /// </summary>
    [CreateAssetMenu(menuName = Shared.MenuPath.Settings + "Game Settings", fileName = "NewGameSettings")]
    public class GameSettings : ScriptableObject 
    {
        [Title("Game Settings")]
        [SerializeField] private EnvironmentState environment = EnvironmentState.Develop;
        [SerializeField] private EnvironmentSettings develop;
        [SerializeField] private EnvironmentSettings staging;
        [SerializeField] private EnvironmentSettings release;

        public static event Action OnChangeEnvironment = delegate { };
        public static EnvironmentState Environment { get; private set; }
        private static Dictionary<EnvironmentState, EnvironmentSettings> Datas { get; set; }
        public static ArenaSettings Arena => Datas[Environment].ArenaSettings;
        public static GeneralSettings General => Datas[Environment].GeneralSettings;
        public static UISettings UI => Datas[Environment].UISettings;
        
        public void OnValidate() => Initialize();
        
        public void Initialize() {
            Environment = environment;

            Datas = new Dictionary<EnvironmentState, EnvironmentSettings>
            {
                { EnvironmentState.Develop, develop },
                { EnvironmentState.Release, release },
                { EnvironmentState.Staging, staging }
            };

            OnChangeEnvironment();
        }
    }
}
