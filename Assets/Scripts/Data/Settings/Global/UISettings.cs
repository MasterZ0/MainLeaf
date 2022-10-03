using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = MenuPath.SettingsGlobal + "UI", fileName = "UISettings")]
    public class UISettings : SerializedScriptableObject
    {
        [Title("UI Settings")]
        [Tooltip("How much will the scroll move when the player presses a scroll button")]
        [SerializeField] private float manualScrollSpeed = 30f;

        [Title("Main Menu")]
        [SerializeField] private Color outlineSelectedColor = Color.green;

        [Title("Settings")]
        [SerializeField] private float rebindTimeOut = 10f;

        [Title("Cutscene")]
        [SerializeField] private float timeToSkipCutscene = 2f;

        [Title("Dialogue")]
        [SerializeField] private float characterDelay = 0.05f;
        [SerializeField] private float commaDelay = 0.1f;
        [Tooltip("Characters: '!', '?', '.'")]
        [SerializeField] private float sentenceDelay = 0.5f;
        [SerializeField] private TMP_ColorGradient itemMaxColor;

        [Title("Fast Dialogue")]
        [SerializeField] private float popupDialogueDuration = 1.5f;

        [Title("Damage Text Popup")]
        [SerializeField] private TMP_ColorGradient playerDefaultDamageColor;
        [DictionaryDrawerSettings]
        [SerializeField] private Dictionary<DamageType, TMP_ColorGradient> damageColors = new Dictionary<DamageType, TMP_ColorGradient>();

        [Title("Effect Text Popup")]
        [DictionaryDrawerSettings]
        [SerializeField] private Dictionary<EffectType, TMP_ColorGradient> effectColors = new Dictionary<EffectType, TMP_ColorGradient>();

        [Title("Notification")]
        [Range(0.1f, 10f), Tooltip("Display gold and collected item name")]
        [SerializeField] private float notificationDuration = 3f;
        [SerializeField] private float delayForWelcomeMessage = 3f;
        [SerializeField] private float delayForDefeatMessage = 2f;
        [SerializeField] private float delayToHideBossLife = 2f;

        // UI Settings
        public Color OutlineSelectedColor => outlineSelectedColor;
        public float ManualScrollSpeed => manualScrollSpeed;

        // Cutscene
        public float TimeToSkipCutscene => timeToSkipCutscene;

        // Dialogue
        public float CharacterDelay => characterDelay;
        public float CommaDelay => commaDelay;
        public float SentenceDelay => sentenceDelay;

        // Fast Dialogue
        public float DialoguePopupDuration => popupDialogueDuration;

        // Text Popup
        public TMP_ColorGradient PlayerDefaultDamageColor => playerDefaultDamageColor;

        public Dictionary<DamageType, TMP_ColorGradient> DamageColors => damageColors;
        public Dictionary<EffectType, TMP_ColorGradient> EffectColors => effectColors;


        // Notification
        public float NotificationDuration => notificationDuration;
        public float DelayForWelcomeMessage => delayForWelcomeMessage;
        public float DelayForDefeatMessage => delayForDefeatMessage;
        public float DelayToHideBossLife => delayToHideBossLife;

        public float RebindTimeOut => rebindTimeOut;
    }
}