using Z3.NodeGraph.Core;
using AdventureGame.Shared.NodeCanvas;
using Z3.NodeGraph.Tasks;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity
{
    [NodeCategory(Categories.Transform)]
    [NodeDescription("Check the distance beetwen the agent to the target comparing the distance.")]
    public class CheckXDistance : ConditionTask<Transform> {

        public Parameter<Vector3> target;
        public Parameter<float> distance;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        public override string Info => $"{AgentInfo}.X - {target}.X" + OperationTools.GetCompareString(checkType) + distance;

        public override bool CheckCondition() {
            float xDistance = Mathf.Abs(Agent.position.x - target.Value.x);
            return OperationTools.Compare(xDistance, distance.Value, checkType, 0f);
        }
    }
}