using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.Projectiles;
using AdventureGame.BattleSystem;
using Z3.UIBuilder.Core;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = Shared.MenuPath.Items + "Stack", fileName = "New" + nameof(ArrowItemData))]
    public class ArrowItemData : ItemData, IQuantifiable
    {
        [Title("Arrow")]
        public ArrowProjectile arrowPrefab;

        // Temp
        public DamageData damage;
        public Vector2 velocity;
        public float delay;
    }
}