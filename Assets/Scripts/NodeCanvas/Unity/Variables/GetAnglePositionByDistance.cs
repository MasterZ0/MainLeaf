using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;
using AdventureGame.Shared.Utils;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Variables)]
    [NodeDescription("Returns a poin with a distance by angle.")]
    public class GetAnglePositionByDistance : ActionTask 
    {
        [Header("In")]
        public Parameter<Vector3> origin;
        public Parameter<float> angle;
        public Parameter<float> distance;
        [Header("Out")]
        public Parameter<Vector3> returnedPosition;

        public override string Info => $"{origin} to {returnedPosition} through angle";

        protected override void StartAction()
        {
            Vector3 newPosition = MathUtils.AngleToDirection(angle.Value, distance.Value);
            returnedPosition.Value = newPosition + origin.Value;
            EndAction(true);
        }
    }
}