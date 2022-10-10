using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Compare the agent position with the target position")]
    public class CheckTargetDirection : ConditionTask<Transform> {

        public BBParameter<Vector3> target;
        public Direction direction;

        protected override string info => $"Targer Direction == {direction}";

        protected override bool OnCheck() {
            switch (direction) {
                case Direction.Up:
                    return target.value.y > agent.position.y;
                case Direction.Down:
                    return target.value.y < agent.position.y;
                case Direction.Left:
                    return target.value.x < agent.position.x;
                case Direction.Right:
                    return target.value.x > agent.position.x;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}