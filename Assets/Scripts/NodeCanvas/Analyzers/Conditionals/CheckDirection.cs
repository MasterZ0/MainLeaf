using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Check if the red axis orientation is in the desired direction.")]
    public class CheckDirection : ConditionTask<Transform> {

        public Parameter<Direction> direction;

        public override string Info => $"Direction == {direction}";

        private const float Min = .5f;
        public override bool CheckCondition() 
        {
            return direction.Value switch
            {
                Direction.Left => Agent.right.x <= -Min,
                Direction.Right => Agent.right.x >= Min,
                Direction.Up => Agent.right.y >= Min,
                Direction.Down => Agent.right.y <= -Min,
                Direction.Forward => Agent.right.z >= Min,
                Direction.Back => Agent.right.z <= -Min,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}