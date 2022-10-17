using NodeCanvas.Framework;
using ParadoxNotion.Design;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [Category(Categories.Analyzers)]
    [Description("Check if the red axis orientation is in the desired direction.")]
    public class CheckDirection : ConditionTask<Transform> {

        public BBParameter<Direction> direction;

        protected override string info => $"Direction == {direction}";

        private const float Min = .5f;
        protected override bool OnCheck() 
        {
            return direction.value switch
            {
                Direction.Left => agent.right.x <= -Min,
                Direction.Right => agent.right.x >= Min,
                Direction.Up => agent.right.y >= Min,
                Direction.Down => agent.right.y <= -Min,
                Direction.Forward => agent.right.z >= Min,
                Direction.Back => agent.right.z <= -Min,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}