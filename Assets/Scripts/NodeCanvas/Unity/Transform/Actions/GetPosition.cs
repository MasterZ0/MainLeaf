using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Get the transform.position.")]
    public class GetPosition : ActionTask<Transform> 
    {
        [Header("Out")]
        public Parameter<Vector3> position;

        public override string Info => $"Get {AgentInfo} Position";
        protected override void StartAction() {
            position.Value = Agent.position;
            EndAction(true);
        }
    }
}