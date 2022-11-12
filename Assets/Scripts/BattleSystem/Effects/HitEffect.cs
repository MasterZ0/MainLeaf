using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.BattleSystem
{
    /// <summary>
    /// When the character takes damage, it will receive this type of effect
    /// </summary>
    [System.Serializable, HideReferenceObjectPicker, InlineProperty]
    public abstract class HitEffect : SkillEffect 
    {
        /// <summary> If true the effect affects the attacker character. </summary>
        public virtual bool AffectSender { get; }
        /// <summary> If true the effect affects the character hitted (the one that receibes the damage) </summary>
        public virtual bool AffectReceiver { get; }
    }
}
