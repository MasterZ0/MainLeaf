using UnityEngine;
using AdventureGame.Shared;
using Z3.UIBuilder.Core;
using System.Collections.Generic;
using TMPro;
using AdventureGame.BattleSystem;

namespace AdventureGame.Data
{
    [CreateAssetMenu(menuName = Shared.MenuPath.SettingsGlobal + "UI", fileName = "UISettings")]
    public class UISettings : ScriptableObject
    {
        [Title("UI Settings")]
        [SerializeField] private Color outlineSelectedColor = Color.green;
        [SerializeField] private float rebindTimeOut = 10f;

        [Title("Damage Text Popup")]
        //[DictionaryDrawerSettings]
        [SerializeField] private Dictionary<DamageType, TMP_ColorGradient> damageColors = new Dictionary<DamageType, TMP_ColorGradient>();


        public Color OutlineSelectedColor => outlineSelectedColor;
        public float RebindTimeOut => rebindTimeOut;
        public Dictionary<DamageType, TMP_ColorGradient> DamageColors => damageColors;
    }
}