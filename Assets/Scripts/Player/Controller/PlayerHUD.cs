using System;
using AdventureGame.BattleSystem;
using AdventureGame.Items;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.Player
{
    [Serializable]
    [FoldoutGroup("HUD"), HideLabel, InlineProperty]
    public class PlayerHUD : PlayerClass
    {
        [Title("Player HUD")]
        [SerializeField] private GameObject redScreen;

        [Title("Bars")]
        [SerializeField] private Slider hpBar;
        [SerializeField] private Slider mpBar;
        [SerializeField] private Slider spBar;

        [Title("Items")]
        [Space]
        [SerializeField] private GameObject arrowContent;
        [SerializeField] private Image arrowIcon;
        [SerializeField] private TMP_Text arrowCount;

        private IAttributes Attributes => Controller.Status.Attributes;
        private PlayerInventory Inventory => Controller.Inventory;

        public override void Init(PlayerController controller)
        {
            base.Init(controller);

            Attributes.OnUpdateStatus += OnUpdateStatus;
            Inventory.OnUpdateInventory += OnUpdateInventory;

            OnUpdateStatus();
            OnUpdateInventory();
        }

        private void OnUpdateStatus()
        {
            float hp = Attributes.HPPercentage();
            hpBar.value = hp;
            mpBar.value = Attributes.MPPercentage();
            spBar.value = Attributes.SPPercentage();

            bool lowHP = hp == 0 || hp <= Controller.PlayerSettings.Visual.LowHealthPercentage * 0.01;
            redScreen.SetActive(lowHP);
        }

        private void OnUpdateInventory()
        {
            UpdateArrow();
        }

        private void UpdateArrow()
        {
            ItemReference arrow = Inventory.Arrow;
            bool hasArrow = arrow;
            arrowContent.SetActive(hasArrow);
            if (hasArrow)
            {
                arrowCount.text = $"x{arrow.amount}";
                arrowIcon.sprite = arrow.Instance.icon;
            }
        }
    }
}