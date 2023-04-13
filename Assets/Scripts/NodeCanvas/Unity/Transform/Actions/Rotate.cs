using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [NodeCategory(Categories.Transform)]
    [NodeDescription("transform.Rotation(euler)")]
    public class Rotate : ActionTask<Transform> {

        public Parameter<Vector3> eulerAngles;

        public override string Info => $"Rotate {eulerAngles}";

        protected override void StartAction() {
            Agent.Rotate(eulerAngles.Value);
            EndAction(true);
        }
    }
}