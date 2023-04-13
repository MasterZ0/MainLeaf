using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Get Character Controller Velocity")]
    public class InverseTransformDirection : ActionTask
    {
        public Parameter<Vector3> direction;
        public Parameter<Vector3> inverse;

        public override string Info => $"Get {AgentInfo} Velocity";

        protected override void StartAction()
        {
            inverse.Value = Agent.transform.InverseTransformDirection(direction.Value);
            EndAction();
        }
    }
}