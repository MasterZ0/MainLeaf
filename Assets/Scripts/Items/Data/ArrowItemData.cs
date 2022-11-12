using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.Projectiles;
using AdventureGame.BattleSystem;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Stack", fileName = "New" + nameof(ArrowItemData))]
    public class ArrowItemData : ItemData, IQuantifiable
    {
        public ArrowProjectile arrowPrefab;

        // Temp
        public DamageData damage;
        public Vector2 velocity;
        public float delay;
    }
}