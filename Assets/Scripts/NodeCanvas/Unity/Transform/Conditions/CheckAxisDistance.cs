using Z3.NodeGraph.Core;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Tasks;
using UnityEngine;
using AdventureGame.Shared.ExtensionMethods;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Check the distance beetwen the reference to the target comparing the selected axis.")]
    public class CheckAxisDistance : ConditionTask
    {
        public Parameter<Vector3> reference;
        public Parameter<Vector3> target;
        public Parameter<float> distance;
        public Parameter<Axis3Flags> axis;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        public override string Info
        {
            get
            {
                return $"{axis} Distance {reference} to {target}" + OperationTools.GetCompareString(checkType) + distance;
            }
        }

        public override bool CheckCondition()
        {
            float axisDistance = axis.Value.Distance(reference.Value, target.Value);
            return OperationTools.Compare(axisDistance, distance.Value, checkType, 0f);
        }
    }
}