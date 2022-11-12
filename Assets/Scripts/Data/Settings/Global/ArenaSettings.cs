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
        [SerializeField, Min(0)] private float spawRadius = 1f;
        [SerializeField, Min(0)] private float maxEnemies = 20f;
        [SerializeField, Min(0)] private float searchPlayerRadius = 10f;

        [Title("Spawn Smoke")]
        [SerializeField, Min(0)] private float smokeWarmingDuration = 2f;
        [SerializeField, Min(0)] private float smokeDisappearsDuration = 2f;
        [SerializeField, Min(0)] private float smokeDelayToDestroy = 3f;

        [Title("Times")]
        [SerializeField, Min(0)] private int secondsToStart = 3;
        [InfoBox("$" + nameof(GetTimePreview))]
        [SerializeField, Min(0)] private float roundDuration = 3f;
        [SerializeField, Range(0f, 10f)] private float gameOverDelay = 3f;

        [Title("Enemies")]
        [SerializeField] private Transform[] enemies;

        public float SpawFrequency => spawFrequency;
        public float SpawRadius => spawRadius;
        public float MaxEnemies => maxEnemies;

        public Transform[] Enemies => enemies;

        public float SmokeWarmingDuration => smokeWarmingDuration;
        public float SmokeDisappearsDuration => smokeDisappearsDuration;
        public float SmokeDelayToDestroy => smokeDelayToDestroy;

        public float SearchPlayerRadius => searchPlayerRadius;

        public int SecondsToStart => secondsToStart;
        public float RoundDuration => roundDuration;
        public float GameOverDelay => gameOverDelay;

        #region Dev Tools
        #if UNITY_EDITOR
        private string GetTimePreview
        {
            get
            {
                var time = System.TimeSpan.FromSeconds(roundDuration);
                return $"Round Duration: {(int)time.TotalMinutes:D2}:{time:ss\\:fff}";
            }
        }
        #endif
        #endregion
        // Ideas: MinimumSpawnDistanceFromPlayer, ChanceToSpawnClosePlayer
    }
}