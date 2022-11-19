using UnityEngine;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;

namespace AdventureGame.Items.Data
{
    [CreateAssetMenu(menuName = MenuPath.Items + "Weapon", fileName = "New" + nameof(WeaponItemData))]
    public class WeaponItemData : ItemData
    {
        [Title(" Weapon")]
        public DamageData damage;
    }
}