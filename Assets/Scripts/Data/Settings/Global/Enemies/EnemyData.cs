using Z3.UIBuilder.Core;
using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.BattleSystem;
using I2.Loc;
using System;
using AdventureGame.Items.Data;

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
    [CreateAssetMenu(menuName = Shared.MenuPath.SettingsSub + "Enemy", fileName = "EnemyData")]
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

        public LocalizedString EnemyName => enemyName;
        public int MaxHealth => maxHealth;
        //public Knockback Knockback => knockback;
        public DropChanceData DropValueData => dropValueData;
        public Gradient HitParticleGradient => hitParticleGradient;
        public DamageData BodyDamage => bodyDamage;

        public event Action OnValueChanged;
        private void OnValidate()
        {
            if (Application.isPlaying)
                OnValueChanged?.Invoke();
        }
    }
}