using AdventureGame.Data;
using AdventureGame.Items;
using System;
using System.Collections.Generic;

namespace AdventureGame.Player
{
    [Serializable]
    public class InventoryData
    {
        // Bag
        public List<ItemReference> Items { get; set; } = new List<ItemReference>();

        // Build
        public ItemReference Weapon { get; set; }
        public ItemReference Arrow { get; set; }

        // Others
        public int Gold { get; set; }

        public InventoryData(DefaultInventory defaultInventory)
        {
            if (defaultInventory.weapon)
            {
                Weapon = defaultInventory.weapon;
                Items.Add(defaultInventory.weapon);
            }

            if (defaultInventory.arrows)
            {
                Arrow = defaultInventory.arrows;
                Arrow.amount = defaultInventory.arrowsCount;
                Items.Add(Arrow);
            }
        }
    }
}