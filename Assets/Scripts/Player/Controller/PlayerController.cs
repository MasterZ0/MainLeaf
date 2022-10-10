using AdventureGame.Inputs;
using UnityEngine;
using AdventureGame.Player.States;
using Sirenix.OdinInspector;
using AdventureGame.BattleSystem;
using AdventureGame.Data;
using System;
using AdventureGame.Items;
using System.Collections.Generic;
using NodeCanvas.StateMachines;

namespace AdventureGame.Player
{
    /// <summary>
    /// Player Core
    /// </summary>
    public class PlayerController : MonoBehaviour, IAttacker, IDamageable, IItemCollector
    {
        [Title("Player Controller")]
        [SerializeField] private Transform center;
        [SerializeField] private FSMOwner stateMachine;
        [SerializeField] private InventoryController playerInventory;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private StatusController playerStatus;

        [SerializeField] private PlayerPhysics playerPhysics;
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private PlayerUI playerUI;
        [SerializeField] private PlayerVFX playerVFX;
        [SerializeField] private PlayerSFX playerSFX;

        [Title("Dev Tools")]
        [SerializeField] private bool debugMode;

        #region Properties
        public int MaxHealth => Status.MaxHP;
        public int CurrentHealth => Status.CurrentHP;
        
        public StatusController Status => playerStatus;
        public PlayerAnimator Animator => playerAnimator;
        public PlayerPhysics Physics => playerPhysics;
        public CameraController Camera => cameraController;
        public PlayerUI UI => playerUI;
        public PlayerVFX VFX => playerVFX;
        public PlayerSFX SFX => playerSFX;
        public InventoryController Inventory => playerInventory;
        public Transform Center => center;
        public Transform Pivot => transform.parent;

        [ShowInInspector, ReadOnly, TextArea]
        private string CurrentState;
        private List<PlayerAction> CurrentActions = new List<PlayerAction>();

        public PlayerAction CurrentPlayerState { get; private set; }


        public FacilitatorBuffer FacilitatorBuffer { get; private set; }
        public PlayerInputs Inputs { get; private set; }

        #endregion

        private float invincibleTime; // stats?

        public event Action<DamageInfo> OnTakeDamage { add => throw new NotImplementedException(); remove => throw new NotImplementedException(); }

        public PlayerSettings PlayerSettings { get; private set; }
        public FSMOwner FSM => stateMachine;


        #region Init
        public void Setup(PlayerSettings settings) //Awake
        {
            PlayerSettings = settings;
            InitComponents();
        }

        private void InitComponents()
        {
            // Instantiates
            Inputs = new PlayerInputs();
            FacilitatorBuffer = new FacilitatorBuffer();

            // Core
            //GameSlotData save = PersistenceManager.Load(new GameSlotData());
            //Inventory.SetInventory(save.playerInventory);
            playerStatus.Init(this);


            // Others
            playerAnimator.Init(this);
            cameraController.Init(this);
            playerPhysics.Init(this);
            playerSFX.Init(this);
            playerUI.Init(this);
            playerVFX.Init(this);

            // Event
            //GameplayController.OnPlayerInputSet += PlayerInputSet;

        }

        private void OnDestroy()
        {
            //GameplayController.OnPlayerInputSet -= PlayerInputSet;
            Inputs.Dispose();
            cameraController.Destroy();
            //playerStatus.Destroy();
            //weaponController.Destroy();
            //playerUI.Destroy();
        }
        #endregion

        private void PlayerInputSet(bool active) => Inputs.SetActive(active);


        private void FixedUpdate()
        {
            if (PlayerSettings)
            {
                playerAnimator.Update();
                cameraController.Update();
                Physics.Update();
                playerStatus.Update();
                FacilitatorBuffer.UpdateBuffers();
            }

            stateMachine.graph.UpdateGraph();
            // status?
            //UpdateInvincible();
        }

        private void UpdateInvincible()
        {
            if (invincibleTime > 0)
            {
                invincibleTime -= Time.fixedDeltaTime;
                if (invincibleTime <= 0)
                {
                    Physics.PlayerDefaultLayer();
                    //OnInvincibleTime = false;
                    //VFX.Flashing(false);
                }
            }
        }

        #region States
        public void EnterState(PlayerAction playerAction) // Lock after request?
        {
            CurrentActions.Add(playerAction);
            UpdateDebug();

            if (debugMode)
            {
                Debug.Log($"State: {CurrentState.GetType().Name}");
            }
        }
        public void ExitState(PlayerAction playerAction) // Lock after request?
        {
            CurrentActions.Remove(playerAction);
            UpdateDebug();
        }

        private void UpdateDebug()
        {
            CurrentState = string.Empty;
            foreach (PlayerAction action in CurrentActions)
            {
                CurrentState += action.GetType().Name + "\n";
            }
        }

        #endregion

        #region Interfaces
        //public bool AddItem(ItemData item, int amount)
        //{
        //    if (item is OrbItemData orbItemData)
        //    {
        //        PlayerReference.Attributes.AddOrb(orbItemData.OrbType);
        //        return true;
        //    }

        //    return Inventory.AddItem(item, amount);
        //}

        public void AddGold(int amount) { }// => Inventory.AddGold(amount);

        public void TakeDamage(Damage damage) // TODO: Shield Script
        {
            // TODO: Shield VFX Block or Hand VFX Block

            if (Status.Dead || Status.GodMode)
                return;

            //if (CanBlockAttack(damage))
            //    return;

            if (Physics.IsPlayerInvincible())
                return;

            playerStatus.TakeDamage(damage);

        }

        public void OnDamageDealt(DamageInfo info)
        {
            Status.OnDamageDealt(info);
        }

        //private bool CanBlockAttack(Damage damage)
        //{
        //    Vector2 damageDirection = GetDamageDirection(damage);
        //    bool canBlockDirection = damageDirection == (Vector2)centerPoint.right;
        //    return Status.IsShielded && Status.HasStamina && canBlockDirection && damage.CanBlock;
        //}

        private Vector2 GetDamageDirection(Damage damage)
        {
            Vector2 contactDirection = damage.ContactPoint - (Vector2)transform.position;
            contactDirection.y = 0;
            contactDirection.Normalize();

            return contactDirection;
        }
        #endregion

        public void Die()
        {
            //SwitchState<DyingPS>();
        }
        

        public void InjuryStunEnd()
        {
            //VFX.Flashing(true);
            //invincibleTime = PlayerSettings.Physics.InvincibilityTimeAfterInjury;
            //Blackboard.OnInvincibleTime = true;
        }

        #region Dev Tools
        [Button]
        private void ResetPlayer()
        {
            Status.RecoveryAllPoints();
            //SwitchState<IdlePS>();
        }

        [Button]
        private void KillPlayer()
        {
            Status.Kill();
        }
        #endregion

        #region Gizmos
        private void OnDrawGizmos()
        {
            playerPhysics.DrawGizmos();
        }

        #endregion
    }
}
