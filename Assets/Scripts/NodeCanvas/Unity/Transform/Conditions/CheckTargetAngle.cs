using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;
using ParadoxNotion;

namespace AdventureGame.NodeCanvas.Unity
{
    [Category(Categories.Transform)]
    [Description("TODO")]
    public class CheckTargetAngle : ConditionTask<Transform>
    {
        public BBParameter<Vector3> target;
        [Range(0f, 180f)]
        public BBParameter<float> angle;
        public CompareMethod checkType = CompareMethod.LessThan;

        protected override string info => $"{target} Angle" + OperationTools.GetCompareString(checkType) + angle;

        protected override bool OnCheck()
        {
            Vector3 directionToCheck = target.value - agent.position;
            float targetAngle = Vector3.Angle(agent.forward, directionToCheck);
            return OperationTools.Compare(targetAngle, angle.value, checkType, 0f);
        }
    }
}