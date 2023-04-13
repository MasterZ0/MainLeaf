using AdventureGame.BattleSystem;
using AdventureGame.Items;
using AdventureGame.Items.Data;
using AdventureGame.Projectiles;
using AdventureGame.Shared.ExtensionMethods;
using Z3.UIBuilder.Core;
using System;
using UnityEngine;
using AdventureGame.ObjectPooling;

namespace AdventureGame.Player
{
    [Serializable]
    //[FoldoutGroup("Arsenal"), HideLabel, InlineProperty]
    public class PlayerArsenal : PlayerClass
    {
        [Title("Arsenal")]
        [SerializeField] private Transform arrowPoint;

        private ItemReference Arrow => Inventory.Arrow;
        private PlayerInventory Inventory => Controller.Inventory;

        public bool CanShootArrow => Arrow && Arrow.amount > 0;

        public void ShootArrow()
        {
            ArrowItemData arrowAsset = (ItemData)Inventory.Arrow as ArrowItemData;
            Inventory.RemoveItem(Arrow);

            ArrowProjectile arrow = ObjectPool.SpawnPooledObject(arrowAsset.arrowPrefab, arrowPoint.position, arrowPoint.rotation);
            Damage damage = new Damage(arrowAsset.damage, Controller);

            float velocity = arrowAsset.velocity.RandomRange();
            arrow.Shoot(damage, velocity, arrowAsset.delay);
        }
    }
}