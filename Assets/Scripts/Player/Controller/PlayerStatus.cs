using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Shared;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AdventureGame.Player
{
    [Serializable]
    [FoldoutGroup("Status"), HideLabel, InlineProperty]
    public class PlayerStatus : BasicStatusController<BasicAttributesController>
    {
        [Title("Status")]
        [SerializeField] private GameObject godModeLabel;

        private PlayerVFX VFX => Controller.VFX;
        private PlayerSFX SFX => Controller.SFX;
        protected override bool Invincible => GodMode || invincibleTime > 0f;

        private PlayerController Controller { get; set; }
        public bool GodMode { get; private set; }

        private DebugInputs debugInputs;
        private float invincibleTime;

        public void Init(PlayerController controller)
        {
            Controller = controller;
            Attributes = new BasicAttributesController();

            PlayerStatusSettings settings = controller.PlayerSettings.Status;
            Attributes.SetMaxHP(settings.BaseHP);
            Attributes.SetHP(settings.BaseHP);

            Attributes.SetMaxMP(settings.BaseMP);
            Attributes.SetMP(settings.BaseMP);

            Attributes.SetMaxSP(settings.BaseSP);
            Attributes.SetSP(settings.BaseSP);

            // Debug Inputs
            debugInputs = new DebugInputs();
            debugInputs.OnGodModeDown += OnSwitchGodMode;
        }

        public void Destroy() => debugInputs.Dispose();

        public override void Update()
        {
            base.Update();
            UpdateInvincibility();
        }

        private void UpdateInvincibility()
        {
            if (invincibleTime > 0)
            {
                invincibleTime -= Time.fixedDeltaTime;
                if (invincibleTime <= 0)
                {
                    InvincibilityEnd();
                }
            }
        }

        private void InvincibilityEnd()
        {
            // Finish: Call VFX and layer? (projectile bool)
        }

        public override bool Restore(AttributePoint attribute, int amount)
        {
            switch (attribute)
            {
                case AttributePoint.HealthPoint:
                    CurrentHP += amount;
                    break;
                case AttributePoint.ManaPoint:
                    CurrentMP += amount;
                    break;
                case AttributePoint.StaminaPoint:
                    CurrentSP += amount;
                    break;
                default:
                    throw new NotImplementedException(attribute.ToString());
            }

            return true;
        }

        protected override void Damage(DamageInfo damageInfo)
        {
            VFX.ReceiveDamage(damageInfo);
            SFX.Injury();

            // Check shield, damage insity, next state event (criticalInjury), knockback direction

            Controller.Injury();
        }

        protected override void Death(DamageInfo damageInfo)
        {
            VFX.ReceiveDamage(damageInfo);
            SFX.Death();

            Controller.Death();
        }

        #region Debug
        private void OnSwitchGodMode()
        {
            GodMode = !GodMode;
            godModeLabel.SetActive(GodMode);
        }
        #endregion
    }

}