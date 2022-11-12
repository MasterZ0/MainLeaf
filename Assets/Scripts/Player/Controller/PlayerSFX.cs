using System;
using AdventureGame.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AdventureGame.Player
{
    [Serializable]
    [FoldoutGroup("Sound Effects"), HideLabel, InlineProperty]
    public class PlayerSFX : PlayerClass
    {
        [Title("SFX")]
        [SerializeField] private SoundReference takeDamage;
        [SerializeField] private SoundReference jump;
        [SerializeField] private SoundReference footStep;
        [SerializeField] private SoundReference landing;
        [SerializeField] private SoundReference dash;
        [SerializeField] private SoundReference shootArrow;

        [Title("Voice")]
        [SerializeField] private SoundReference attackVoice;
        [SerializeField] private SoundReference injuryVoice;
        [SerializeField] private SoundReference deathVoice;

        private Transform transform;

        public override void Init(PlayerController controller)
        {
            transform = controller.Center;
        }

        #region Standard Sounds
        public void FoodStep() => footStep.PlaySound(transform);
        public void Jump() => jump.PlaySound(transform);
        public void Landing() => landing.PlaySound(transform);
        public void Dash() => dash.PlaySound(transform);
        public void ShootArrow() => shootArrow.PlaySound(transform);
        #endregion

        #region Gender Sounds
        public void Death() 
        {
            takeDamage.PlaySound(transform);
            deathVoice.PlaySound(transform);
        }

        public void Injury() 
        {
            takeDamage.PlaySound(transform);
            injuryVoice.PlaySound(transform);
        }

        public void AttackVoice() => attackVoice.PlaySound(transform);
        #endregion
    }
}