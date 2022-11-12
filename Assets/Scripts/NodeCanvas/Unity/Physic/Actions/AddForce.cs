using AdventureGame.Shared.NodeCanvas;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [Category(Categories.Rigidbody)]
    [Description("Add Force")]
    public class AddForce : ActionTask<Rigidbody>
    {
        public BBParameter<Vector3> force;
        public BBParameter<ForceMode> forceMode = ForceMode.Force;

        protected override string info => $"Add Force = {force}, {forceMode}";

        protected override void OnExecute()
        {
            agent.AddForce(force.value, forceMode.value);
            EndAction(true);
        }
    }
}