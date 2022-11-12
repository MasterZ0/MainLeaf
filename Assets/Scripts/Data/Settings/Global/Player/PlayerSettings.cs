using Sirenix.OdinInspector;
using UnityEngine;
using AdventureGame.Shared;
using I2.Loc;

namespace AdventureGame.Data
{
    /// <summary>
    /// Stores all player Basic Settings
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.SettingsPlayers + "Archer", fileName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private LocalizedString characterName;

        [TabGroup("Player", "Status"), HideLabel, InlineProperty]
        [SerializeField] private PlayerStatusSettings status;

        [TabGroup("Player", "Physics"), HideLabel, InlineProperty]
        [SerializeField] private PlayerPhysicsSettings physics;

        [TabGroup("Player", "Visual"), HideLabel, InlineProperty]
        [SerializeField] private PlayerVisualSettings visual;

        [TabGroup("Player", "Default Inventory"), HideLabel, InlineProperty]
        [SerializeField] private DefaultInventory defaultInventory;

        //[TabGroup("Player", "Action Windows"), HideLabel, InlineProperty]
        //[SerializeField] private PlayerActionWindowSettings actionWindows;

        public LocalizedString CharacterName => characterName;
        public PlayerStatusSettings Status => status;
        public PlayerPhysicsSettings Physics => physics;
        public PlayerVisualSettings Visual => visual;
        public DefaultInventory DefaultInventory => defaultInventory;
    }
}