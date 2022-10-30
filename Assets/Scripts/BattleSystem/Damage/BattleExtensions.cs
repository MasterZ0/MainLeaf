using UnityEngine;
namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// Extensions to make interfaces easier to use <see cref="IHittable"/>, <see cref="IDamageable"/>, <see cref="IAttacker"/> and <see cref="IBattleEntity"/>
    /// </summary>
    public static class BattleExtensions
    {
        public static void TakeDamage(this IHittable hittable, int value)
        {
            Damage damage = new Damage(value);
            hittable.TakeDamage(damage);
        }

        /// <param name="damagePercentage"> 0 - 100 </param>
        public static void TakeDamagePercentage(this IDamageable damageable, float damagePercentage)
        {
            int damageValue = Mathf.FloorToInt(damageable.CurrentHealth * damagePercentage * 0.01f);
            damageable.TakeDamage(damageValue);
        }

        public static void Kill(this IDamageable damageable)
        {
            damageable.TakeDamage(damageable.CurrentHealth);
        }

        public static bool IsDead(this IDamageable damageable)
        {
            return damageable.CurrentHealth <= 0f;
        }
    }
}