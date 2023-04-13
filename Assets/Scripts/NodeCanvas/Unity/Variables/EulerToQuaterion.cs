using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Convert Euler Angles in a Quaterion")]
    public class EulerToQuaterion : ActionTask
    {
        public Parameter<Vector3> euler;
        public Parameter<Quaternion> quaterion;

        public override string Info => $"{quaterion} = Quaternion.Euler({euler})";

        protected override void StartAction()
        {
            quaterion.Value = Quaternion.Euler(euler.Value);
            EndAction(true);
        }
    }
}