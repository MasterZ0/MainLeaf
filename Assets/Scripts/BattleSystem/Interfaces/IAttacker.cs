using UnityEngine;

namespace AdventureGame.BattleSystem
{
    public interface IAttacker : IBattleEntity
    {
        // AttackerInfo: atributes?, etc... 
        // OnAttack(melee/range) // Enemy dodge
        void OnDamageDealt(DamageInfo info);
    }
}