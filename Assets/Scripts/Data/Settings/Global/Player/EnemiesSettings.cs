using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + "Enemies", fileName = "EnemiesSettings")]
    public class EnemiesSettings : ScriptableObject
    {
        [Title("Enemies Settings")]
        [SerializeField] private WarriorSkeletonSettings warriorSkeleton;
        [SerializeField] private MageSkeletonSettings mageSkeletonSettings;

        public WarriorSkeletonSettings WarriorSkeleton => warriorSkeleton;
        public MageSkeletonSettings MageSkeletonSettings => mageSkeletonSettings;
    }
}