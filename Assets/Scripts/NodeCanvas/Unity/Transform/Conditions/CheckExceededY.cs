using NodeCanvas.Framework;
using AdventureGame.Shared.NodeCanvas;
using ParadoxNotion.Design;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Unity {

    [Category(Categories.Transform)]
    [Description("Compare the agent passed the Y target + offset.")]
    public class CheckExceededY : ConditionTask<Transform> {

        public BBParameter<Vector3> target;
        public BBParameter<float> offset;

        protected override string info => !offset.isDefined && offset.value == 0 ?
            $"Exceeded Y {target}" :
            $"Exceeded Y {target} + {offset}";

        protected override bool OnCheck() {
            float inverseY = agent.InverseTransformPoint(target.value).y;
            return inverseY <= offset.value;
        }
    }
}