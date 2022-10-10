using UnityEngine;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;
using System;

namespace AdventureGame.AI
{
    /// <summary>
    /// Component used in characters with multiple body parts
    /// </summary>
    public class BodyPart : MonoBehaviour, IHittable 
    {
        [Title("Body Part")]
        [SerializeField] private IHittable parent;
        [SerializeField] private Transform center;

        public event Action<DamageInfo> OnTakeDamage;

        public Transform Center => center;
        public Transform Pivot => transform;

        public void TakeDamage(Damage damage)
        {
            parent.TakeDamage(damage);
        }
    }
}