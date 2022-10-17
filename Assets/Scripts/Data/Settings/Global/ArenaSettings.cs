using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + "Arena", fileName = "ArenaSettings")]
    public class ArenaSettings : ScriptableObject
    {
        [Title("Arena Settings")]
        [SerializeField, Range(0.1f, 10f)] private float spawFrequency = 1f;
        [SerializeField, Min(0)] private float spawRadius = 1;
        [SerializeField, Min(0)] private float maxEnemies = 20;

        [Title("Spawn Smoke")]
        [SerializeField, Min(0)] private float smokeWarmingDuration = 2;
        [SerializeField, Min(0)] private float smokeDisappearsDuration = 2;
        [SerializeField, Min(0)] private float smokeDelayToDestroy = 3;

        [Title("Enemies")]
        [SerializeField] private Transform[] enemies;

        public float SpawFrequency => spawFrequency;
        public float SpawRadius => spawRadius;
        public float MaxEnemies => maxEnemies;

        public Transform[] Enemies => enemies;

        public float SmokeWarmingDuration => smokeWarmingDuration;
        public float SmokeDisappearsDuration => smokeDisappearsDuration;
        public float SmokeDelayToDestroy => smokeDelayToDestroy;

        // Ideas: MinimumSpawnDistanceFromPlayer, ChanceToSpawnClosePlayer
    }
}