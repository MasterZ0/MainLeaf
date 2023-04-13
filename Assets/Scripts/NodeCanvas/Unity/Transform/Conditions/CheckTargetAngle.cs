using Z3.NodeGraph.Core;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("TODO")]
    public class CheckTargetAngle : ConditionTask<Transform>
    {
        public Parameter<Vector3> target;
        [Range(0f, 180f)]
        public Parameter<float> angle;
        public CompareMethod checkType = CompareMethod.LessThan;

        public override string Info => $"{target} Angle" + OperationTools.GetCompareString(checkType) + angle;

        public override bool CheckCondition()
        {
            Vector3 directionToCheck = target.Value - Agent.position;
            float targetAngle = Vector3.Angle(Agent.forward, directionToCheck);
            return OperationTools.Compare(targetAngle, angle.Value, checkType, 0f);
        }
    }
}