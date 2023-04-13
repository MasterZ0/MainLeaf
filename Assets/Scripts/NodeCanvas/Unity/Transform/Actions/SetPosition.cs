using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [NodeCategory(Categories.Transform)]
    [NodeDescription("Set Transform.position")]
    public class SetPosition : ActionTask<Transform> {

        public Parameter<Vector3> position;

        public override string Info => $"Position = {position}";

        protected override void StartAction() {
            Agent.position = position.Value;
            EndAction(true);
        }
    }
}