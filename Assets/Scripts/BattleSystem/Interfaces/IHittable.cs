using System;
using UnityEngine;

namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// Anything that takes damage implements this interface. You need a Rigidbody to work.
    /// </summary>
    public interface IHittable
    {
        event Action<DamageInfo> OnTakeDamage;
        public Transform Center { get; }
        public Transform Pivot { get; }
        void TakeDamage(Damage damage);
    }
}