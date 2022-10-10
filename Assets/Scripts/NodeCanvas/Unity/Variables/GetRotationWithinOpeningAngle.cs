using AdventureGame.Shared.NodeCanvas;
using AdventureGame.Shared.ExtensionMethods;
using AdventureGame.Shared.Utils;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using HeaderAttribute = ParadoxNotion.Design.HeaderAttribute;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Variables)]
    [Description("Get a quatertion with Z rotation inside of a opening angle")]
    public class GetRotationWithinOpeningAngle : ActionTask
    {
        [Header("In")]
        public BBParameter<Transform> directedTransform;
        public BBParameter<float> desiredAngle;
        public BBParameter<float> openingAngle;

        [Header("Out")]
        public BBParameter<Quaternion> rotation;

        protected override string info => $"{name} = {openingAngle}";

        protected override void OnExecute()
        {
            float angleZ = MathUtils.DirectionToAngle(directedTransform.value.right);
            
            float halfAngle = openingAngle.value / 2;
            float minAngle = (angleZ - halfAngle).NormalizeAngle();
            float maxAngle = (angleZ + halfAngle).NormalizeAngle();

            float eulerZ = desiredAngle.value;
            Vector2 angleRange = new Vector2(minAngle, maxAngle);
            if (!angleRange.InsideRange(eulerZ))
            {
                float a = MathUtils.AngleDiference(desiredAngle.value, angleRange.x);
                float b = MathUtils.AngleDiference(desiredAngle.value, angleRange.y);

                eulerZ = a < b ? angleRange.x : angleRange.y;
            }

            rotation.value = Quaternion.Euler(0f,0f, eulerZ);
            EndAction(true);
        }
    }
}