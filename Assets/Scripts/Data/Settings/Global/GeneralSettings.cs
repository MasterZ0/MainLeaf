using Sirenix.OdinInspector;
using UnityEngine;
using AdventureGame.Shared;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + "General", fileName = "GeneralSettings")]
    public class GeneralSettings : ScriptableObject
    {
        [Title("General")]
        [SerializeField] private float tripleGargoyleTriggerTime = 5f;

        [Title("Achievements")]
        [Min(1)]
        [SerializeField] private int highLevel = 99;
        [Min(1)]
        [SerializeField] private int highGold = 50000;
        [Min(1)]
        [SerializeField] private int notesCount = 26;
        [Min(1)]
        [SerializeField] private int oldLadiesKilled = 50;

        [Title("VFX")]
        [SerializeField] private float hitMaterialSeconds;

        [Title("Physics")]
        [Range(0, 2f)]
        [SerializeField] private float strongKnockback = 0.4f;
        [Range(0, 2f)]
        [SerializeField] private float mediumKnockback = 0.15f;
        [Range(0, 2f)]
        [SerializeField] private float weakKnockback = 0.05f;

        [Title("Cave Ritual")] 
        [Range(0f, 100f), SuffixLabel("%")]
        [SerializeField] private float caveRitualDamagePercentage;

        public float TripleGargoyleTriggerTime => tripleGargoyleTriggerTime;
        public int HighLevel => highLevel;
        public int HighGold => highGold;
        public int NotesCount => notesCount;
        public int OldLadiesKilled => oldLadiesKilled;

        public float HitMaterialSeconds => hitMaterialSeconds;
        
        public float StrongKnockback => strongKnockback;
        public float MediumKnockback => mediumKnockback;
        public float WeakKnockback => weakKnockback;

        public float CaveRitualDamagePercentage => caveRitualDamagePercentage;
    }
}