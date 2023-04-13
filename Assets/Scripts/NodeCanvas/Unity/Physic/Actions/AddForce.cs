using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity.Physic
{
    [NodeCategory(Categories.Rigidbody)]
    [NodeDescription("Add Force")]
    public class AddForce : ActionTask<Rigidbody>
    {
        public Parameter<Vector3> force;
        public Parameter<ForceMode> forceMode = ForceMode.Force;

        public override string Info => $"Add Force = {force}, {forceMode}";

        protected override void StartAction()
        {
            Agent.AddForce(force.Value, forceMode.Value);
            EndAction(true);
        }
    }
}