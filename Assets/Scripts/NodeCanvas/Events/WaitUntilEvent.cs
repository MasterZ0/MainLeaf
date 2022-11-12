using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;

namespace AdventureGame.NodeCanvas.Utils
{
    [Category(Categories.Events)]
    [Description("Waits for a graph event")]
    public class WaitUntilEvent : ActionTask<GraphOwner>
    {
        [RequiredField] public BBParameter<string> eventName;

        protected override string info => $"Wait until [{eventName}]";

        protected override void OnExecute()
        {
            router.onCustomEvent += OnCustomEvent;
        }

        protected override void OnStop()
        {
            router.onCustomEvent -= OnCustomEvent;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            if (sentEventName.Equals(eventName.value, System.StringComparison.OrdinalIgnoreCase))
                EndAction(true);
        }
    }

    [Category(Categories.Events)]
    [Description("Waits for a graph event")]
    public class WaitUntilEvent<T> : ActionTask<GraphOwner>
    {
        [RequiredField] public BBParameter<string> eventName;
        public BBParameter<T> outValue;

        protected override string info => $"Wait until <<b>{outValue.varType.Name}</b>> [{eventName}]";

        protected override void OnExecute()
        {
            router.onCustomEvent += OnCustomEvent;
        }

        protected override void OnStop()
        {
            router.onCustomEvent -= OnCustomEvent;
        }

        private void OnCustomEvent(string sentEventName, IEventData data)
        {
            if (sentEventName.Equals(eventName.value, System.StringComparison.OrdinalIgnoreCase) && data is EventData<T> eventData)
            {
                outValue.value = eventData.value;
                EndAction(true);
            }
        }
    }
}