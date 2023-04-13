using AdventureGame.Items;
using Z3.UIBuilder.Core;
using System;
using System.Linq;

namespace AdventureGame.Player
{
    [Serializable]
    //[FoldoutGroup("Inventory"), HideLabel, InlineProperty]
    public class PlayerInventory : PlayerClass, IInventoryController
    {
        [Title("Inventory")]
        private InventoryData Inventory { get; set; }

        public Action OnUpdateInventory = delegate { };

        public ItemReference Weapon => Inventory.Weapon;
        public ItemReference Arrow { get => Inventory.Arrow; private set => Inventory.Arrow = value; }

        public override void Init(PlayerController controller)
        {
            Inventory = new InventoryData(controller.PlayerSettings.DefaultInventory);
        }

        public void Destroy() { }

        #region Gold
        public void AddGold(int amount) => Inventory.Gold += amount;
        public bool CollectGold(int amount)
        {
            if (Inventory.Gold >= amount)
            {
                Inventory.Gold -= amount;
                return true;
            }
            return false;
        }
        #endregion

        public bool AddItem(ItemReference newItem)
        {
            if (!newItem.CanStack() || !Inventory.Items.Contains(newItem))
            {
                Inventory.Items.Add(newItem);
            }
            else
            {
                ref int amount = ref newItem.amount;

                // Fill other items
                foreach (ItemReference item in Inventory.Items.Where(i => i == newItem && !newItem.MaxAmount()))
                {
                    int slotCount = item.SlotCount();
                    if (slotCount < amount) // Max Count
                    {
                        amount -= slotCount;
                        item.amount += slotCount;
                    }
                    else // Less Max
                    {
                        item.amount += amount;
                        amount = 0;
                        newItem = item; // TryEquip same item in inventory as new
                        break;
                    }
                }

                if (amount > 0)
                {
                    Inventory.Items.Add(newItem);
                }
            }

            TryEquip(newItem);
            OnUpdateInventory();

            return true;
        }

        public void RemoveItem(ItemReference item, int amount = 1)
        {
            RemoveRecursive(item, amount);
            CheckUnequip();
            OnUpdateInventory();
        }

        private void RemoveRecursive(ItemReference item, int amount)
        {
            ItemReference other = Inventory.Items.First(i => i == item);

            if (amount >= other.amount)
            {
                amount -= other.amount;
                other.amount = 0;
                Inventory.Items.Remove(other);
                if (amount > 0)
                {
                    RemoveRecursive(item, amount);
                }
            }
            else
            {
                other.amount -= amount;
            }
        }

        private void TryEquip(ItemReference newItem)
        {
            if (!Arrow)
            {
                Arrow = newItem;
            }
        }

        private void CheckUnequip()
        {
            if (Arrow.amount <= 0)
            {
                Arrow = null;
            }
        }

        private void ChangeArrow(ref ItemReference field, ItemReference newItem) // Ideas
        {
            if (newItem == field)
                return;

            field?.Dispose();
            field = newItem;
            field?.Instantiate();
        }
    }
}