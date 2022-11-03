    using NodeCanvas.Framework;

namespace AdventureGame.Player.States
{
    public class IdlePS : PlayerAction
    {
        public BBParameter<string> idleState;
        public BBParameter<string> overrideIdleState;

        protected override void EnterState()
        {
            string stateName = string.IsNullOrEmpty(overrideIdleState.value) ? idleState.value : overrideIdleState.value;
            Animator.PlayAllLayers(stateName);
            overrideIdleState.value = string.Empty;
            EndAction();
        }
    }
}