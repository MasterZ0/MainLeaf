using AdventureGame.Items.Data;

namespace AdventureGame.Items
{
    public interface IInventoryOwner 
    {
        IInventoryController Inventory { get; }
    }
}