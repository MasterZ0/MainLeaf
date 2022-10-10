using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;
using ParadoxNotion;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("Check the distance beetwen the agent to the target comparing the distance.")]
    public class CheckXDistance : ConditionTask<Transform> {

        public BBParameter<Vector3> target;
        public BBParameter<float> distance;
        public CompareMethod checkType = CompareMethod.LessOrEqualTo;

        protected override string info => $"{agentInfo}.X - {target}.X" + OperationTools.GetCompareString(checkType) + distance;

        protected override bool OnCheck() {
            float xDistance = Mathf.Abs(agent.position.x - target.value.x);
            return OperationTools.Compare(xDistance, distance.value, checkType, 0f);
        }
    }
}