using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Check if the red axis orientation is in the desired direction.")]
    public class CheckDirection : ConditionTask<Transform> {

        public Direction direction;

        protected override string info => $"Direction == {direction}";

        private const float Min = .5f;
        protected override bool OnCheck() {
            switch (direction) {
                case Direction.Up:
                    return agent.right.y >= Min;
                case Direction.Down:
                    return agent.right.y <= -Min;
                case Direction.Left:
                    return agent.right.x <= -Min;
                case Direction.Right:
                    return agent.right.x >= Min;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}