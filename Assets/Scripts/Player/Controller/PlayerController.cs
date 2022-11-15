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
using AdventureGame.Gameplay;
using AdventureGame.Items.Data;

namespace AdventureGame.Player
{
    public enum PlayerEvent
    {
        Injury,
        Death
    }

    /// <summary>
    /// Player Core
    /// </summary>
    public class PlayerController : MonoBehaviour, IStatusOwner, IInventoryOwner, IPlayer
    {
        [Title("Player Controller")]
        [CustomBox]
        [SerializeField] private DataSettings<PlayerSettings> settings;
        [Space]
        [SerializeField] private Transform center;
        [SerializeField] private Transform head;
        [SerializeField] private FSMOwner stateMachine;
        [SerializeField] private PlayerEvents playerEvents;
        [Space]
        [SerializeField] private PlayerInventory playerInventory;
        [SerializeField] private PlayerArsenal playerArsenal;
        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private PlayerStatus playerStatus;

        [SerializeField] private PlayerPhysics playerPhysics;
        [SerializeField] private PlayerAnimator playerAnimator;
        [SerializeField] private PlayerHUD playerHUD;
        [SerializeField] private PlayerVFX playerVFX;
        [SerializeField] private PlayerSFX playerSFX;

        [Title("Dev Tools")]
        [SerializeField] private bool debugMode;

        #region Properties
        public event Action<DamageInfo> OnTakeDamage = delegate { };
        public event Action<PlayerEvent> OnPlayerEvent = delegate { };

        public FSMOwner FSM => stateMachine; // Remove??
        public PlayerStatus Status => playerStatus;
        public PlayerAnimator Animator => playerAnimator;
        public PlayerPhysics Physics => playerPhysics;
        public PlayerCamera Camera => playerCamera;
        public PlayerHUD UI => playerHUD;
        public PlayerVFX VFX => playerVFX;
        public PlayerSFX SFX => playerSFX;
        public PlayerInventory Inventory => playerInventory;
        public PlayerArsenal Arsenal => playerArsenal;

        // Interface
        public Transform Pivot => transform;
        public Transform Center => center;
        public Transform Head => head;

        public PlayerSettings PlayerSettings { get; private set; }
        public FacilitatorBuffer FacilitatorBuffer { get; private set; }
        public PlayerInputs Inputs { get; private set; }

        IStatusController IStatusOwner.Status => playerStatus;
        IInventoryController IInventoryOwner.Inventory => playerInventory;
        #endregion

        [ShowInInspector, ReadOnly, TextArea]
        private string CurrentState;
        private List<PlayerAction> CurrentActions = new List<PlayerAction>();

        #region Init
        private void Awake()
        {
            PlayerSettings = settings;
            InitComponents();
        }

        private void InitComponents()
        {
            // Instantiates
            Inputs = new PlayerInputs(GameplayReferences.InputActive);
            FacilitatorBuffer = new FacilitatorBuffer();

            // Core
            //GameSlotData save = PersistenceManager.Load(new GameSlotData());
            playerInventory.Init(this);
            playerStatus.Init(this);

            // Others
            playerEvents.Init(this);
            playerArsenal.Init(this);
            playerAnimator.Init(this);
            playerCamera.Init(this);
            playerPhysics.Init(this);
            playerSFX.Init(this);
            playerHUD.Init(this);
            playerVFX.Init(this);

            // Event
            GameplayReferences.RegisterPlayer(this);
            GameplayReferences.OnPlayerInputSet += PlayerInputSet;
        }

        private void OnDestroy()
        {
            GameplayReferences.UnregisterPlayer(this);
            GameplayReferences.OnPlayerInputSet -= PlayerInputSet;

            Inputs.Dispose();
            playerStatus.Destroy();
            playerInventory.Destroy();
        }
        #endregion

        private void PlayerInputSet(bool active) => Inputs.SetActive(active);

        private void FixedUpdate()
        {
            stateMachine.graph.UpdateGraph();

            Physics.Update();
            playerStatus.Update();
            FacilitatorBuffer.Update();
            playerCamera.Update();
        }

        #region Interfaces
        public bool AddItem(ItemReference item) => Inventory.AddItem(item);
        public void AddGold(int amount) => Inventory.AddGold(amount);
        #endregion

        #region States
        public void Injury() => OnPlayerEvent.Invoke(PlayerEvent.Injury);
        public void Death()
        {
            GameplayReferences.PlayerChangeState(this);
            OnPlayerEvent.Invoke(PlayerEvent.Death);
        }

        // Ideas
        public void EnterState(PlayerAction playerAction) // Lock after request?
        {
            CurrentActions.Add(playerAction);
            UpdateDebug();

            if (debugMode)
            {
                Debug.Log($"Enter State: {CurrentState.GetType().Name}");
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

        #region Dev Tools
        [Button]
        private void ResetPlayer()
        {
            Status.Attributes.RecoveryAllPoints();
            stateMachine.RestartBehaviour();
        }

        [Button]
        private void KillPlayer() => this.Kill();
        #endregion

        #region Gizmos
        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
                playerPhysics.DrawGizmos();
        }
        #endregion
    }
}
