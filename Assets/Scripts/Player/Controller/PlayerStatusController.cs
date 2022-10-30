using AdventureGame.BattleSystem;
using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Player.States;
using AdventureGame.Shared;
using AdventureGame.Shared.ExtensionMethods;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AdventureGame.Player
{
    [System.Serializable]
    [FoldoutGroup("Status"), HideLabel, InlineProperty]
    public class StatusController
    {
        [Title("Status")]
        [SerializeField] private GameEvent onGameOver;
        [SerializeField] private GameObject godMoveLabel;

        public bool GodMode { get; private set; }
        public int MaxHP { get; private set; } = 1;
        public int CurrentHP { get; private set; } = 1;
        public bool Dead { get; internal set; }

        private DebugInputs Inputs;
        public void GameOver() => onGameOver.Invoke();

        private PlayerController playerController;

        public void Init(PlayerController controller)
        {
            playerController = controller;

            // Debug Inputs
            Inputs = new DebugInputs();
            Inputs.OnGodModeDown += OnSwitchGodMode;

        }


        #region Debug
        private void OnSwitchGodMode()
        {
            GodMode = !GodMode;
            godMoveLabel.SetActive(GodMode);
        }

        public void TakeDamage(Damage damage)
        {
            bool isDead = false;
            if (isDead)
            {
                Kill();
            }
            else
            {
                ApplyDamage(damage.ContactPoint.Value);
            }
        }

        public void ApplyDamage(Vector3 contactPoint) // TODO: Refactor
        {
            //bool rightContact = contactPoint.x > transform.position.x;
            //BackDamage = rightContact != playerPhysics.LookRight;

            //bool criticalInjury = damage.Strong || !Physics.CheckGround();
            //bool applyStun = criticalInjury || !CurrentState.CombatState || Status.IsShielded;

            // status?
            //Quaternion quaternion = Quaternion.AngleAxis(rightContact ? 180f : 0f, Vector2.up);
            //VFX.ReceiveDamage(damage.ContactPoint, quaternion);

            //if (!criticalInjury)
            //{
            //    Physics.Knockback(PlayerSettings.Physics.SlightInjuryDisplacement, rightContact ? 1 : -1);
            //}

            //if (!applyStun)
            //{
            //    InjuryStunEnd();
            //    return;
            //}

            //playerController.SwitchState<InjuryPS>();
        }

        public void OnDamageDealt(DamageInfo info)
        {
        }

        internal void Update()
        {
        }

        internal void RecoveryAllPoints()
        {
        }

        internal void Kill()
        {
            playerController.Die();
        }
        #endregion
    }

}