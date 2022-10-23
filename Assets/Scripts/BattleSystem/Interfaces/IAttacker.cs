using UnityEngine;

namespace AdventureGame.BattleSystem
{
    public interface IAttacker : IBattleEntity
    {
        // AttackerInfo: atributes?, etc... 
        // OnAttack(melee/range) // Where: on enabled hitbox -> Used to: Enemy dodge
        void OnDamageDealt(DamageInfo info);
    }
}