using Sirenix.OdinInspector;
using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.BattleSystem;
using I2.Loc;
using System;
using System.Collections.Generic;

namespace AdventureGame.Data
{
    public enum Knockback
    {
        Disable,
        Weak,
        Medium,
        Strong
    }

    /// <summary>
    /// Enemy Data
    /// </summary>
    [CreateAssetMenu(menuName = MenuPath.SettingsSub + "Enemy", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject 
    {
        [Title("Enemy")] 
        [SerializeField] private LocalizedString enemyName;
        [SerializeField, Range(1f, 10000f)] private int maxHealth = 1;
        [SerializeField] private Gradient hitParticleGradient = new Gradient { colorKeys = new[] { new GradientColorKey(Color.red, 0) } };

        [Header("Drop")]
        [SerializeField] private DropChanceData dropValueData;

        [Header("Body")]
        //[SerializeField] private Knockback knockback;
        [SerializeField] private DamageData bodyDamage;

        public event Action OnValueChanged;
        private void OnValidate() => OnValueChanged?.Invoke();

        public LocalizedString EnemyName => enemyName;
        public int MaxHealth => maxHealth;
        public DropChanceData DropValueData => dropValueData;
        //public Knockback Knockback => knockback;
        public Gradient HitParticleGradient => hitParticleGradient;
        public DamageData BodyDamage => bodyDamage;
    }

    [Serializable, InlineProperty, HideLabel]
    public class DropChanceData // Loot
    {
        [SerializeField, MinMaxSlider(0, 20, true)] private Vector2Int bronzeCoinsRange;
        [SerializeField, MinMaxSlider(0, 20, true)] private Vector2Int silverCoinsRange;
        [SerializeField, MinMaxSlider(0, 20, true)] private Vector2Int goldCoinsRange;
        //[SerializeField] private List<ItemDropChance> droppedItems;
        //[SerializeField] private List<ObjectDropChance> droppedObjects;

        public Vector2Int BronzeCoinsRange => bronzeCoinsRange;
        public Vector2Int SilverCoinsRange => silverCoinsRange;
        public Vector2Int GoldCoinsRange => goldCoinsRange;
        public List<Transform> DroppedObjects => throw new NotImplementedException();
        //public List<ItemDropValue> DroppedItems => throw new NotImplementedException();
        //public List<ItemDropChance> DroppedChanceItems => droppedItems;
        //public List<ObjectDropChance> DroppedChanceObjects => droppedObjects;

        //public int BronzeCoins => bronzeCoinsRange.RandomRange();
        //public int SilverCoins => silverCoinsRange.RandomRange();
        //public int GoldCoins => goldCoinsRange.RandomRange();
    }
}