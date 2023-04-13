using Z3.NodeGraph.Core;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Compare the Inverse Transform Point to target + offset.")]
    public class CheckInversePointAxis : ConditionTask<Transform>
    {
        public Parameter<Axis3> axis;
        public Parameter<Vector3> target;
        public Parameter<float> offset;
        public Parameter<float> value;
        public CompareMethod checkType = CompareMethod.EqualTo;

        public override string Info => $"Inverse Point {axis} {target}" + OperationTools.GetCompareString(checkType) + $"{value}";
        public override bool CheckCondition()
        {
            Vector3 inverse = Agent.InverseTransformPoint(target.Value);

            float axisDistance = offset.Value + axis.Value switch
            {
                Axis3.X => inverse.x,
                Axis3.Y => inverse.y,
                Axis3.Z => inverse.z,
                _ => throw new System.NotImplementedException(),
            };

            return OperationTools.Compare(axisDistance, value.Value, checkType, 0f);
        }
    }
}