using UnityEngine;
using AdventureGame.Shared;
using AdventureGame.BattleSystem;
using Sirenix.OdinInspector;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Status", fileName = "New" + nameof(StatusItemData))]
    public class StatusItemData : ItemData
    {
        [Title("Status")]

        public AttributePoint attributePoint;
        public int restoreValue;
    }
}