using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.BattleSystem;
using Z3.UIBuilder.Core;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = Shared.MenuPath.Items + "Status", fileName = "New" + nameof(StatusItemData))]
    public class StatusItemData : ItemData
    {
        [Title("Status")]

        public AttributePoint attributePoint;
        public int restoreValue;
    }
}