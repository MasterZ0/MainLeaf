using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.Player.States
{
    [NodeDescription("It becomes true when the selected input changes to the desired state, disregarding the current state")]
    public class CheckPlayerEventPS : PlayerCondition
    {
        public Parameter<PlayerEvent> eventType;

        private bool actionCalled;

        public override string Info => $"{name}: {eventType}";

        public override void StartCondition()
        {
            actionCalled = false;
            Agent.OnPlayerEvent += OnPlayerEvent;
        }

        public override void StopCondition()
        {
            Agent.OnPlayerEvent -= OnPlayerEvent;
        }

        private void OnPlayerEvent(PlayerEvent playerEvent) => actionCalled = playerEvent == eventType.Value;

        public override bool CheckCondition()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}