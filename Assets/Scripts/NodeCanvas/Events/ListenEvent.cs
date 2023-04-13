using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;

namespace AdventureGame.NodeCanvas.Utils
{
    [NodeCategory(Categories.Events)]
    [NodeDescription("Stores an event until it's consumed")]
    public class ListenEvent : ConditionTask<GraphOwner>
    {
        /*[RequiredField]*/ public Parameter<string> eventName;

        public override string Info => $"[{eventName}]";
        private bool eventReceived;

        //protected override void OnEnable() => router.onCustomEvent += OnCustomEvent;
        //protected override void OnDisable() => router.onCustomEvent -= OnCustomEvent;

        public override bool CheckCondition()
        {
            bool lastEventReceived = eventReceived;
            eventReceived = false;
            return lastEventReceived;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            if (sentEventName.Equals(eventName.Value, System.StringComparison.OrdinalIgnoreCase))
                eventReceived = true;
        }
    }
    
    [NodeCategory(Categories.Events)]
    [NodeDescription("Stores an event until it's consumed")]
    public class ListenEvent<T> : ConditionTask<GraphOwner> 
    {
        /*[RequiredField]*/ public Parameter<string> eventName;
        public Parameter<T> outValue;

        //public override string Info => $"Event [{eventName}]\n{outValue} = {outValue.varType.Name}Event";
        private bool eventReceived;
        private T value;

        //protected override void OnEnable() => router.onCustomEvent += OnCustomEvent;
        //protected override void OnDisable() => router.onCustomEvent -= OnCustomEvent;

        public override bool CheckCondition()
        {
            if (!eventReceived) 
                return false;
            
            outValue.Value = value;
            eventReceived = false;
            return true;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            //if (sentEventName.Equals(eventName.Value, System.StringComparison.OrdinalIgnoreCase) && data is EventData<T> eventData)
            //{
            //    eventReceived = true;
            //    value = eventData.Value;
            //}
        }
    }
}