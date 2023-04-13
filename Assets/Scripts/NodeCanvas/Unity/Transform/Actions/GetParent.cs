using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Return the parent's transform from the Agent.")]
    public class GetParent : ActionTask <Transform>
    {
        public Parameter<Transform> returnParent;
        protected override void StartAction() {
            returnParent.Value = Agent.parent;
            EndAction(true);
        }
    }
}