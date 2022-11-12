using AdventureGame.Shared;
using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Utils
{
    [Category(Categories.Events)]
    [Description("Waits for a graph event")]
    public class WaitAnimationTrigger : ActionTask<AnimationEventTrigger>
    {
        [RequiredField] public BBParameter<string> eventName;

        protected override string info => $"Wait Animation Trigger [{eventName}]";

        protected override void OnExecute()
        {
            agent.OnEventTrigger += OnEventTrigger;
        }

        protected override void OnStop()
        {
            agent.OnEventTrigger -= OnEventTrigger;
        }

        private void OnEventTrigger(string sentEventName)
        {
            if (sentEventName.Equals(eventName.value, System.StringComparison.OrdinalIgnoreCase))
                EndAction(true);
        }
    }
}