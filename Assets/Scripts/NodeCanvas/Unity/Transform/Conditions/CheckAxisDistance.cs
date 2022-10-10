using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;
using ParadoxNotion;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Check the distance beetwen the reference to the target comparing the selected axis.")]
    public class CheckAxisDistance : ConditionTask
    {
        public BBParameter<Vector3> reference;
        public BBParameter<Vector3> target;
        public BBParameter<float> distance;
        public BBParameter<Axis> axis;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        protected override string info
        {
            get
            {
                string startText = axis.value == Axis.Both ? "Distance" : $"{axis} Distance";
                return $"{startText} {reference} to {target}" + OperationTools.GetCompareString(checkType) + distance;
            }
        }

        protected override bool OnCheck()
        {
            float axisDistance = axis.value switch
            {
                Axis.X => Mathf.Abs(reference.value.x - target.value.x),
                Axis.Y => Mathf.Abs(reference.value.y - target.value.y),
                Axis.Both => Vector2.Distance(reference.value, target.value),
                _ => throw new System.NotImplementedException(),
            };

            return OperationTools.Compare(axisDistance, distance.value, checkType, 0f);
        }
    }
}