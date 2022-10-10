using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Check if the agent is inside of limits")]
    public class IsInside : ConditionTask<Transform> {

        public BBParameter<Vector3> leftUpLimit;
        public BBParameter<Vector3> rightDownLimit;

        public BBParameter<Axis> axis;

        protected override bool OnCheck() {

            switch (axis.value) {
                case Axis.X:
                    return InsideX();
                case Axis.Y:
                    return InsideY();
                case Axis.Both:
                    return InsideX() && InsideY();
                default:
                    throw new System.NotImplementedException();
            }
        }

        private bool InsideX() => agent.position.x > leftUpLimit.value.x && agent.position.x < rightDownLimit.value.x;
        private bool InsideY() => agent.position.y < leftUpLimit.value.y && agent.position.y > rightDownLimit.value.y;
    }
}