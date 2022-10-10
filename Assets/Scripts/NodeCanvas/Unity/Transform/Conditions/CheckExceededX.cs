using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Transform)]
    [Description("Compare the agent passed the X target + offset.")]
    public class CheckExceededX : ConditionTask<Transform> {

        public BBParameter<Vector3> target;
        public BBParameter<float> offset;

        protected override string info => !offset.isDefined && offset.value == 0 ?
            $"Exceeded X {target}" : offset.value < 0 ?
            $"Exceeded X {target} {offset}" :
            $"Exceeded X {target} +{offset}";

        protected override bool OnCheck() {
            float inverseX = agent.InverseTransformPoint(target.value).x;
            return inverseX <= offset.value;
        }
    }
}