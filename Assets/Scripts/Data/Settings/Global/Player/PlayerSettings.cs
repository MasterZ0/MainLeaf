using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using AdventureGame.Shared;
using I2.Loc;

namespace AdventureGame.Data
{
    /// <summary>
    /// Stores all player Basic Settings
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + "Player", fileName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] private LocalizedString characterName;

        [TabGroup("Player", "Status"), HideLabel, InlineProperty]
        [SerializeField] private PlayerStatusSettings status;

        [TabGroup("Player", "Physics"), HideLabel, InlineProperty]
        [SerializeField] private PlayerPhysicsSettings physics;

        [TabGroup("Player", "Visual"), HideLabel, InlineProperty]
        [SerializeField] private PlayerVisualSettings visual;

        //[TabGroup("Player", "Action Windows"), HideLabel, InlineProperty]
        //[SerializeField] private PlayerActionWindowSettings actionWindows;

        public LocalizedString CharacterName => characterName;
        public PlayerStatusSettings Status => status;
        public PlayerPhysicsSettings Physics => physics;
        public PlayerVisualSettings Visual => visual;
    }
}