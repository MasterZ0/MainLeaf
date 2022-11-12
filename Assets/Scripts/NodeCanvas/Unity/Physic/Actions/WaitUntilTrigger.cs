using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Rigidbody)]
    [Description("Wait until anything is inside the Trigger area and return the Transform.")]
    public class WaitUntilTrigger : ActionTask<Collider>
    {
        public BBParameter<bool> triggerExit;
        public BBParameter<Collider> returnedCollider;

        protected override string info => $"Wait Until Trigger {(triggerExit.value ? "Exit" : "Enter")} {agentInfo}";

        protected override void OnExecute()
        {
            if (!triggerExit.value)
                router.onTriggerEnter += OnTrigger;
            else
                router.onTriggerExit += OnTrigger;
        }

        protected override void OnStop()
        {
            router.onTriggerExit -= OnTrigger;
            router.onTriggerEnter -= OnTrigger;
        }

        private void OnTrigger(EventData<Collider> data)
        {
            returnedCollider.value = data.value;
            EndAction();
        }
    }
}