using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody2D)]
    [Description("Wait until anything is inside the Trigger area and return the Transform.")]
    public class WaitUntilTrigger2D : ActionTask<Collider2D>
    {
        public BBParameter<bool> triggerExit = false;
        public BBParameter<Transform> returnedTransform;

        protected override string info => $"Wait Until Trigger {(triggerExit.value ? "Exit" : "Enter")} {agentInfo}";

        protected override void OnExecute()
        {
            if(!triggerExit.value)
                router.onTriggerStay2D += OnTriggerStay2D;
            else
                router.onTriggerExit2D += OnTriggerExit2D;
        }

        private void OnTriggerStay2D(EventData<Collider2D> data)
        {
            router.onTriggerStay2D -= OnTriggerStay2D;
            FinishTrigger(data);
        }
        
        private void OnTriggerExit2D(EventData<Collider2D> data)
        {
            router.onTriggerExit2D -= OnTriggerExit2D;
            FinishTrigger(data);
        }

        private void FinishTrigger(EventData<Collider2D> data)
        {
            returnedTransform.value = data.value.transform;
            EndAction(true);
        }
    }
}