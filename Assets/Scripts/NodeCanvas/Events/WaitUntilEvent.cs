using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Utils
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Waits for a graph event")]
    public class WaitUntilEvent : ActionTask<GraphOwner>
    {
        /*[RequiredField]*/ public Parameter<string> eventName;

        public override string Info => $"Wait until [{eventName}]";

        protected override void StartAction()
        {
            //router.onCustomEvent += OnCustomEvent;
        }

        protected override void StopAction()
        {
            //router.onCustomEvent -= OnCustomEvent;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            if (sentEventName.Equals(eventName.Value, System.StringComparison.OrdinalIgnoreCase))
                EndAction(true);
        }
    }

    [NodeCategory(Categories.Events)]
    [NodeDescription("Waits for a graph event")]
    public class WaitUntilEvent<T> : ActionTask<GraphOwner>
    {
        /*[RequiredField]*/ public Parameter<string> eventName;
        public Parameter<T> outValue;

        //public override string Info => $"Wait until <<b>{outValue.varType.Name}</b>> [{eventName}]";

        protected override void StartAction()
        {
            //router.onCustomEvent += OnCustomEvent;
        }

        protected override void StopAction()
        {
            //router.onCustomEvent -= OnCustomEvent;
        }

        //private void OnCustomEvent(string sentEventName, IEventData data)
        //{
        //    if (sentEventName.Equals(eventName.Value, System.StringComparison.OrdinalIgnoreCase) && data is EventData<T> eventData)
        //    {
        //        outValue.Value = eventData.Value;
        //        EndAction(true);
        //    }
        //}
    }
}