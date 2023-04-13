using Z3.UIBuilder.Core;
using UnityEngine;
using AdventureGame.Shared;

namespace AdventureGame.Data {

    /// <summary>
    /// Storage all development environment data
    /// </summary>
    //[InlineEditor(objectFieldMode: InlineEditorObjectFieldModes.Boxed)]
    [CreateAssetMenu(menuName = Shared.MenuPath.Settings + "Environment Settings", fileName = "EnvironmentSettings")]
    public class EnvironmentSettings : ScriptableObject 
    {
        [SerializeField] private ArenaSettings arenaSettings;
        [SerializeField] private GeneralSettings generalSettings;
        [SerializeField] private UISettings uiSettings;

        public ArenaSettings ArenaSettings => arenaSettings;
        public GeneralSettings GeneralSettings => generalSettings;
        public UISettings UISettings => uiSettings;
    }
}
