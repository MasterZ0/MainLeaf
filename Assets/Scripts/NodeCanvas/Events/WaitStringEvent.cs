using AdventureGame.Shared;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Utils
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Waits for a graph event")]
    public class WaitStringEvent : ActionTask<StringEvent>
    {
        /*[RequiredField]*/ public Parameter<string> eventName;

        public override string Info => $"Wait Animation Event [{eventName}]";

        protected override void StartAction()
        {
            Agent.OnEventTrigger += OnEventTrigger;
        }

        protected override void StopAction()
        {
            Agent.OnEventTrigger -= OnEventTrigger;
        }

        private void OnEventTrigger(string sentEventName)
        {
            if (sentEventName.Equals(eventName.Value, System.StringComparison.OrdinalIgnoreCase))
                EndAction(true);
        }
    }
}