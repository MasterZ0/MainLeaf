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
        [SerializeField] private Transform head;

        public event Action<DamageInfo> OnTakeDamage;

        public Transform Pivot => transform;
        public Transform Center => head ?? transform;
        public Transform Head => head ?? transform;

        public void TakeDamage(Damage damage)
        {
            parent.TakeDamage(damage);
        }
    }
}