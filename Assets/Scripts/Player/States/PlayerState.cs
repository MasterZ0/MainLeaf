using AdventureGame.Data;
using AdventureGame.Inputs;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using System.Linq;

namespace AdventureGame.Player.States
{
    [NodeCategory(Categories.PlayerStates)]
    public abstract class PlayerCondition : ConditionTask<PlayerController>
    {
        #region Player Components
        protected PlayerStatus Status => Agent.Status;
        protected PlayerArsenal Arsenal => Agent.Arsenal;
        protected PlayerPhysics Physics => Agent.Physics;
        protected PlayerInputs Inputs => Agent.Inputs;
        protected FacilitatorBuffer FacilitatorBuffer => Agent.FacilitatorBuffer;
        #endregion

        #region Settings
        protected PlayerSettings Settings => Agent.PlayerSettings;
        protected PlayerPhysicsSettings PhysicsSettings => Settings.Physics;
        #endregion
    }


    /// <summary>
    /// Note to developers: Please describe what this class does.
    /// </summary>

    [NodeCategory(Categories.PlayerStates)]
    public abstract class PlayerAction : ActionTask<PlayerController>
    {
        #region Player Components
        protected PlayerStatus Status => Agent.Status;
        protected PlayerCamera Camera => Agent.Camera;
        protected PlayerPhysics Physics => Agent.Physics;
        protected PlayerAnimator Animator => Agent.Animator;
        protected PlayerVFX VFX => Agent.VFX;
        protected PlayerSFX SFX => Agent.SFX;
        protected PlayerInputs Inputs => Agent.Inputs;
        protected FacilitatorBuffer FacilitatorBuffer => Agent.FacilitatorBuffer;
        #endregion

        #region Settings
        protected PlayerSettings Settings => Agent.PlayerSettings;
        protected PlayerPhysicsSettings PhysicsSettings => Settings.Physics;
        #endregion


        protected sealed override void StartAction()
        {
            Agent.EnterState(this);
            EnterState();
        }

        protected sealed override void StopAction()
        {
            Agent.ExitState(this);
            ExitState();
        }

        // There could exist a history with the class and exit time
        //private bool GetPreviousState<T>() => (Agent.FSM.GetPreviousState() as ActionState).actionList.actions.Any(s => s.GetType() == typeof(T));

        protected virtual void EnterState() { }
        protected virtual void ExitState() { }

    }
}