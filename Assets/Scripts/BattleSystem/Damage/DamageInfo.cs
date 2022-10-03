using UnityEngine;

namespace AdventureGame.BattleSystem
{
    public class DamageInfo
    {
        public Damage Damage { get; set; }
        public Transform Receiver { get; set; }
        public DamageInfo(Damage damage, Transform receiver)
        {
            Damage = damage;
            Receiver = receiver;
        }
    }
}