using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity { 

    [NodeCategory(Categories.Transform)]
    [NodeDescription("Set transform.localScale")]
    public class SetScale : ActionTask<Transform> {

        public Parameter<Vector3> scale;

        protected override void StartAction() {
            Agent.localScale = scale.Value;
            EndAction(true);
        }
    }
}