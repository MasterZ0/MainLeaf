using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.Player.States
{
    [Description("It becomes true when the selected input changes to the desired state, disregarding the current state")]
    public class CheckPlayerEventPS : PlayerCondition
    {
        public BBParameter<PlayerEvent> eventType;

        private bool actionCalled;

        protected override string info => $"{name}: {eventType}";

        protected override void OnEnable()
        {
            actionCalled = false;
            agent.OnPlayerEvent += OnPlayerEvent;
        }

        protected override void OnDisable()
        {
            agent.OnPlayerEvent -= OnPlayerEvent;
        }

        private void OnPlayerEvent(PlayerEvent playerEvent) => actionCalled = playerEvent == eventType.value;

        protected override bool OnCheck()
        {
            bool value = actionCalled;
            actionCalled = false;
            return value;
        }
    }
}