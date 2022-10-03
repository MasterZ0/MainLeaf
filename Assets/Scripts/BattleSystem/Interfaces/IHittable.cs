using System;

namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// Anything that takes damage implements this interface. You need a Rigidbody to work.
    /// </summary>
    public interface IHittable 
    {
        event Action<DamageInfo> OnTakeDamage;
        DamageInfo TakeDamage(Damage damage);
    }
}