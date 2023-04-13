    using Z3.NodeGraph.Core;

namespace AdventureGame.Player.States
{
    public class IdlePS : PlayerAction
    {
        public Parameter<string> idleState;
        public Parameter<string> overrideIdleState;

        protected override void EnterState()
        {
            string stateName = string.IsNullOrEmpty(overrideIdleState.Value) ? idleState.Value : overrideIdleState.Value;
            Animator.PlayAllLayers(stateName);
            overrideIdleState.Value = string.Empty;
            EndAction();
        }
    }
}