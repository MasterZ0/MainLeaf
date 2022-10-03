using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// When the character takes damage, it will receive this type of effect
    /// </summary>
    [System.Serializable, HideReferenceObjectPicker, InlineProperty]
    public abstract class HitEffect : SkillEffect {
        // If true the effect affects the character hitted (the one that receibes the damage)
        [SerializeField] private bool affectHitted = true;
        public bool AffectHitted => affectHitted;

        // If true the effect affects the attacker character 
        [SerializeField] private bool affectAttacker = false;
        public bool AffectAttacker => affectAttacker;
    }
}
