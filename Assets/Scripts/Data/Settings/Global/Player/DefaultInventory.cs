using AdventureGame.Items.Data;

namespace AdventureGame.Data
{
    [System.Serializable]
    public class DefaultInventory
    {
        public WeaponItemData weapon;
        public ArrowItemData arrows;
        public int arrowsCount;
    }
}