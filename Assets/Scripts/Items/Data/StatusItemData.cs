using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.BattleSystem;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Status", fileName = "New" + nameof(StatusItemData))]
    public class StatusItemData : ItemData
    {
        public AttributePoint attributePoint;
        public int restoreValue;
    }
}