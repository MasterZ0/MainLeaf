using System;
using AdventureGame.Data;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.Player
{
    [Serializable]
    [FoldoutGroup("UI"), HideLabel, InlineProperty]
    public class PlayerUI
    {
        [Title("Player UI")]
        [SerializeField] private GameObject redScreen;

        [Title("Bars")]
        [SerializeField] private Slider hpBar;
        [SerializeField] private Slider mpBar;
        [SerializeField] private Slider spBar;

        [Title("Items")]
        [Space]
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TextMeshProUGUI arrowCount;

        [Title("Boost")]
        [SerializeField] private GameObject aimReticle;

        public void SetActiveReticle(bool enable) => aimReticle.SetActive(enable);

        internal void Init(PlayerController playerController)
        {

        }

        private void OnUpdateBaseAttributes()
        {
            //hpBar.value = Attributes.HPPercentage;
            //mpBar.value = Attributes.MPPercentage;
            //spBar.value = Attributes.SPPercentage;
        }

        private void HideStamina()
        {
            spBar.gameObject.SetActive(false);
        }

        public void UpdateHP(float percentage)
        {
            hpBar.value = percentage;
            //bool lowLife = percentage <= Visual.LowHealthPercentage / 100f;
            //bool dead = percentage == 0;
            //redScreen.SetActive(lowLife && !dead);
        }

        public void UpdateMP(float percentage)
        {
            mpBar.value = percentage;
        }

        public void UpdateSP(float percentage)
        {
            spBar.value = percentage;
        }
    }
}