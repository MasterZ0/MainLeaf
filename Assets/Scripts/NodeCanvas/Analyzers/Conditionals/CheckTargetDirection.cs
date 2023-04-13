using Z3.NodeGraph.Core;
using Z3.NodeGraph.Tasks;
using AdventureGame.Shared.NodeCanvas;
using UnityEngine;

namespace AdventureGame.NodeCanvas.Analyzers {

    [NodeCategory(Categories.Analyzers)]
    [NodeDescription("Compare the agent position with the target position")]
    public class CheckTargetDirection : ConditionTask<Transform> {

        public Parameter<Vector3> target;
        public Parameter<Direction> direction;

        public override string Info => $"Targer Direction == {direction}";

        public override bool CheckCondition() 
        {
            return direction.Value switch
            {
                Direction.Left => target.Value.x < Agent.position.x,
                Direction.Right => target.Value.x > Agent.position.x,
                Direction.Up => target.Value.y > Agent.position.y,
                Direction.Down => target.Value.y < Agent.position.y,
                Direction.Forward => target.Value.z < Agent.position.z,
                Direction.Back => target.Value.z > Agent.position.z,
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}