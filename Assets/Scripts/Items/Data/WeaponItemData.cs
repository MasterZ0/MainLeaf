using UnityEngine;
using AdventureGame.Shared;
using Z3.UIBuilder.Core;
using AdventureGame.BattleSystem;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = Shared.MenuPath.Items + "Weapon", fileName = "New" + nameof(WeaponItemData))]
    public class WeaponItemData : ItemData
    {
        [Title(" Weapon")]
        public DamageData damage;
    }
}