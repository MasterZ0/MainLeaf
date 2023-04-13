using System;
using System.Collections.Generic;
using System.Linq;
using AdventureGame.Audio;
using Z3.UIBuilder.Core;
using UnityEngine;

namespace AdventureGame.Player
{
    [Serializable]
    //[FoldoutGroup("Sound Effects"), HideLabel, InlineProperty]
    public class PlayerSFX : PlayerClass
    {
        [Title("SFX")]
        [SerializeField] private SoundReference damage;
        [SerializeField] private SoundReference jump;
        [SerializeField] private SoundReference landingSoft;
        [SerializeField] private SoundReference landingHard;
        [SerializeField] private SoundReference dash;

        [Title("Item")]
        [SerializeField] private SoundReference bowPrepare;
        [SerializeField] private SoundReference bowShoot;
        [SerializeField] private SoundReference bowRelease;

        [Title("Voice")]
        [SerializeField] private SoundReference restoreVoice;
        [SerializeField] private SoundReference bowPrepareVoice;
        [SerializeField] private SoundReference bowShootVoice;
        [SerializeField] private SoundReference jumpVoice;
        [SerializeField] private SoundReference landingSoftVoice;
        [SerializeField] private SoundReference landingHardVoice;
        [SerializeField] private SoundReference injurySlightVoice;
        [SerializeField] private SoundReference injuryMediumVoice;
        [SerializeField] private SoundReference injuryCriticalVoice;
        [SerializeField] private SoundReference deathVoice;

        private List<ActiveSoundInstance> activeSoundInstances = new List<ActiveSoundInstance>();

        private Transform transform;
        private PlayerAnimator Animator => Controller.Animator;

        private const string Aim = "Aim";

        public override void Init(PlayerController controller)
        {
            base.Init(controller);
            transform = controller.Center;
        }

        public void Update()
        {
            foreach (ActiveSoundInstance activeSoundInstance in activeSoundInstances.ToList())
            {
                if (!Animator.IsState(activeSoundInstance.StateName, activeSoundInstance.Layer))
                {
                    activeSoundInstance.SoundInstances.ForEach(i => i.StopWithFade());
                    activeSoundInstances.Remove(activeSoundInstance);
                }
            }
        }

        #region Standard Sounds
        public void Restore()
        {
            restoreVoice.PlaySound(transform);
        }

        public void Jump()
        {
            jumpVoice.PlaySound(transform);
            jump.PlaySound(transform);
        }

        public void LandingSoft()
        {
            landingSoftVoice.PlaySound(transform);
            landingSoft.PlaySound(transform);
        }

        public void LandingHard()
        {
            landingHardVoice.PlaySound(transform);
            landingHard.PlaySound(transform);
        }

        public void Dash() => dash.PlaySound(transform);
        public void PrepareArrow()
        {
            SoundInstance itemSoundInstance = bowPrepare.PlaySound(transform);
            SoundInstance voiceSoundInstance = bowPrepareVoice.PlaySound(transform);

            ActiveSoundInstance activeSoundInstance = new ActiveSoundInstance(Aim, 2, itemSoundInstance, voiceSoundInstance);
            activeSoundInstances.Add(activeSoundInstance);
        }

        public void ShootArrow()
        {
            bowShoot.PlaySound(transform);
            bowShootVoice.PlaySound(transform);
        }
        #endregion

        #region Gender Sounds
        public void Death() 
        {
            damage.PlaySound(transform);
            deathVoice.PlaySound(transform);
        }

        public void Injury() 
        {
            damage.PlaySound(transform);
            injurySlightVoice.PlaySound(transform);
        }
        #endregion
    }

    public class ActiveSoundInstance
    {
        public string StateName { get; }
        public int Layer { get; }
        public List<SoundInstance> SoundInstances { get; }

        public ActiveSoundInstance(string stateName, int layer, params SoundInstance[] instances)
        {
            StateName = stateName;
            Layer = layer;
            SoundInstances = instances.ToList();
        }
    }
}