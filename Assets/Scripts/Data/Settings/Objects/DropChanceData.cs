using Sirenix.OdinInspector;
using UnityEngine;
using System;
using System.Collections.Generic;
using AdventureGame.Items.Data;

namespace AdventureGame.Data
{
    [Serializable, InlineProperty, HideLabel]
    public class DropChanceData
    {
        [MinMaxSlider(0, 20, true)]
        [SerializeField] private Vector2Int goldRange;

        [SerializeField] private List<DropChance<DropItem>> loot;

        public Vector2Int GoldRange => GoldRange;
        public List<DropChance<DropItem>> Loot => loot;
    }

    [Serializable]
    public class DropChance<T>
    {
        public T drop;
        [Range(0, 100)]
        public float chance;
    }

    [Serializable, InlineProperty, HideLabel]
    public class DropItem
    {
        public ItemData item;
        [MinMaxSlider(0, 1000, true), ShowIf(nameof(ShowAmount))]
        public Vector2Int amountRange;

        private bool ShowAmount => item is IQuantifiable;
    }
}