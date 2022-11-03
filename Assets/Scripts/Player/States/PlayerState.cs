using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using System.Linq;

namespace AdventureGame.Player.States
{
    [Category(Categories.PlayerStates)]
    public abstract class PlayerCondition : ConditionTask<PlayerController>
    {
        #region Player Components
        protected PlayerSettings Settings => agent.PlayerSettings;
        protected StatusController Status => agent.Status;
        protected CameraController Camera => agent.Camera;
        protected PlayerPhysics Physics => agent.Physics;
        protected PlayerAnimator Animator => agent.Animator;
        protected PlayerVFX VFX => agent.VFX;
        protected PlayerSFX SFX => agent.SFX;
        protected PlayerInputs Inputs => agent.Inputs;
        protected FacilitatorBuffer FacilitatorBuffer => agent.FacilitatorBuffer;
        #endregion

        #region Settings
        protected PlayerPhysicsSettings PhysicsSettings => Settings.Physics;
        #endregion

        public virtual bool CombatState => false;
    }


    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>

    [Category(Categories.PlayerStates)]
    public abstract class PlayerAction : ActionTask<PlayerController>
    {
        #region Player Components
        protected PlayerSettings Settings => agent.PlayerSettings;
        protected StatusController Status => agent.Status;
        protected CameraController Camera => agent.Camera;
        protected PlayerPhysics Physics => agent.Physics;
        protected PlayerAnimator Animator => agent.Animator;
        protected PlayerVFX VFX => agent.VFX;
        protected PlayerSFX SFX => agent.SFX;
        protected PlayerInputs Inputs => agent.Inputs;
        protected FacilitatorBuffer FacilitatorBuffer => agent.FacilitatorBuffer;
        #endregion

        #region Settings
        protected PlayerPhysicsSettings PhysicsSettings => Settings.Physics;
        #endregion

        protected sealed override void OnExecute()
        {
            agent.EnterState(this);
            EnterState();
        }

        protected sealed override void OnStop()
        {
            agent.ExitState(this);
            ExitState();
        }

        // There could exist a history with the class and exit time
        private bool GetPreviousState<T>() => (agent.FSM.GetPreviousState() as ActionState).actionList.actions.Any(s => s.GetType() == typeof(T));

        protected virtual void EnterState() { }
        protected virtual void ExitState() { }

    }
}