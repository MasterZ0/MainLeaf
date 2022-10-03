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
        [SerializeField] private GeneralSettings generalSettings;
        [SerializeField] private CharacterSettings playerSettings;
        [SerializeField] private UISettings uiSettings;

        public GeneralSettings GeneralSettings => generalSettings;
        public CharacterSettings PlayerSettings => playerSettings;
        public UISettings UISettings => uiSettings;
    }
}
