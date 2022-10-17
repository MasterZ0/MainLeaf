using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;

namespace AdventureGame.Data
{
    /// <summary>
    /// Stores all player Basic Settings
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + "Players", fileName = "PlayersSettings")]
    public class PlayersSettings : ScriptableObject
    {
        [Title("Players Settings")]
        [SerializeField] private PlayerSettings archer;
        [SerializeField] private PlayerSettings mage;
        [SerializeField] private PlayerSettings warrior;

        public PlayerSettings Archer => archer;
        public PlayerSettings Mage => mage;
        public PlayerSettings Warrior => warrior;
    }
}