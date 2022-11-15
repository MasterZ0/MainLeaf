using AdventureGame.BattleSystem;

namespace AdventureGame.Items
{
    public interface IInventoryOwner : IBattleEntity
    {
        IInventoryController Inventory { get; }
    }
}