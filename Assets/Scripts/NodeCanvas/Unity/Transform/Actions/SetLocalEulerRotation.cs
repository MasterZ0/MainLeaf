using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Set Transform.localRotation using Euler")]
    public class SetLocalEulerRotation : ActionTask<Transform>
    {
        public Parameter<Vector3> rotation;

        public override string Info => $"Euler Local Rotation = {rotation}";

        protected override void StartAction()
        {
            Agent.localRotation = Quaternion.Euler(rotation.Value);
            EndAction(true);
        }
    }
}