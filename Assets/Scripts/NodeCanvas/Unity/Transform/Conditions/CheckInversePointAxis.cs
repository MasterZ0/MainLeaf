using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;
using ParadoxNotion;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Compare the Inverse Transform Point to target + offset.")]
    public class CheckInversePointAxis : ConditionTask<Transform>
    {
        public BBParameter<Axis3> axis;
        public BBParameter<Vector3> target;
        public BBParameter<float> offset;
        public BBParameter<float> value;
        public CompareMethod checkType = CompareMethod.EqualTo;

        protected override string info => $"Inverse Point {axis} {target}" + OperationTools.GetCompareString(checkType) + $"{value}";
        protected override bool OnCheck()
        {
            Vector3 inverse = agent.InverseTransformPoint(target.value);

            float axisDistance = offset.value + axis.value switch
            {
                Axis3.X => inverse.x,
                Axis3.Y => inverse.y,
                Axis3.Z => inverse.z,
                _ => throw new System.NotImplementedException(),
            };

            return OperationTools.Compare(axisDistance, value.value, checkType, 0f);
        }
    }
}