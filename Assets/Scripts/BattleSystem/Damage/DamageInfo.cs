using UnityEngine;

namespace AdventureGame.BattleSystem
{
    public class DamageInfo
    {
        public Damage Damage { get; set; }
        public IDamageable Receiver { get; set; }
        public int EffectiveDamage { get; set; }

        /// <summary> Nullable </summary>
        public IAttacker Sender => Damage.Sender;

        public DamageInfo(Damage damage, IDamageable receiver, int effectiveDamage)
        {
            Damage = damage;
            Receiver = receiver;
            EffectiveDamage = effectiveDamage;

            Sender?.OnDamageDealt(this);
        }
    }
}