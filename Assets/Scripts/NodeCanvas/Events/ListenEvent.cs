using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Utils
{
    [Category(Categories.Events)]
    [Description("Stores an event until it's consumed")]
    public class ListenEvent : ConditionTask<GraphOwner>
    {
        [RequiredField] public BBParameter<string> eventName;

        protected override string info => $"[{eventName}]";
        private bool eventReceived;

        protected override void OnEnable() => router.onCustomEvent += OnCustomEvent;
        protected override void OnDisable() => router.onCustomEvent -= OnCustomEvent;

        protected override bool OnCheck()
        {
            bool lastEventReceived = eventReceived;
            eventReceived = false;
            return lastEventReceived;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            if (sentEventName.Equals(eventName.value, System.StringComparison.OrdinalIgnoreCase))
                eventReceived = true;
        }
    }
    
    [Category(Categories.Events)]
    [Description("Stores an event until it's consumed")]
    public class ListenEvent<T> : ConditionTask<GraphOwner> 
    {
        [RequiredField] public BBParameter<string> eventName;
        public BBParameter<T> outValue;

        protected override string info => $"Event [{eventName}]\n{outValue} = {outValue.varType.Name}Event";
        private bool eventReceived;
        private T value;

        protected override void OnEnable() => router.onCustomEvent += OnCustomEvent;
        protected override void OnDisable() => router.onCustomEvent -= OnCustomEvent;

        protected override bool OnCheck()
        {
            if (!eventReceived) 
                return false;
            
            outValue.value = value;
            eventReceived = false;
            return true;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            if (sentEventName.Equals(eventName.value, System.StringComparison.OrdinalIgnoreCase) && data is EventData<T> eventData)
            {
                eventReceived = true;
                value = eventData.value;
            }
        }
    }
}