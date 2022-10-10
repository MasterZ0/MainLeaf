using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;
using AdventureGame.Shared.Utils;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Returns a poin with a distance by angle.")]
    public class GetAnglePositionByDistance : ActionTask 
    {
        [Header("In")]
        public BBParameter<Vector3> origin;
        public BBParameter<float> angle;
        public BBParameter<float> distance;
        [Header("Out")]
        public BBParameter<Vector3> returnedPosition;

        protected override string info => $"{origin} to {returnedPosition} through angle";

        protected override void OnExecute()
        {
            Vector3 newPosition = MathUtils.AngleToDirection(angle.value, distance.value);
            returnedPosition.value = newPosition + origin.value;
            EndAction(true);
        }
    }
}