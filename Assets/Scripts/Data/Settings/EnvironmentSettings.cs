using Sirenix.OdinInspector;
using UnityEngine;
using AdventureGame.Shared;

namespace AdventureGame.Data {

    /// <summary>
    /// Storage all development environment data
    /// </summary>
    [InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(menuName = MenuPath.Settings + "Environment Settings", fileName = "EnvironmentSettings")]
    public class EnvironmentSettings : ScriptableObject 
    {
        [SerializeField] private ArenaSettings arenaSettings;
        [SerializeField] private EnemiesSettings enemiesSettings;
        [SerializeField] private GeneralSettings generalSettings;
        [SerializeField] private PlayersSettings playersSettings;
        [SerializeField] private UISettings uiSettings;

        public ArenaSettings ArenaSettings => arenaSettings;
        public EnemiesSettings EnemiesSettings => enemiesSettings;
        public GeneralSettings GeneralSettings => generalSettings;
        public PlayersSettings PlayersSettings => playersSettings;
        public UISettings UISettings => uiSettings;
    }
}
